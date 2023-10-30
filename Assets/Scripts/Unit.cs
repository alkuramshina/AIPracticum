using System;
using Configuration;
using Enums;
using UnityEngine;

public class Unit : ClickableElement
{
    private Transform _defaultTarget;
    
    private Vector3 _currentPositionTarget;
    private Unit _currentUnitTarget;
    
    private UnitSettings _unitSettings;
    
    private int _health;
    private UnitState _state;

    private float _attackDistance = 2f;
    private float _seekingDistance = 10f;

    public EventHandler OnDeath;

    protected override void Update()
    {
        UpdateState();

        UpdateFightingBehavior();
        UpdateMovingBehavior();
        
        base.Update();
    }

    private void UpdateState()
    {
        // Если допреследовались до расстояния атаки, переходим в атаку
        if (_state == UnitState.Pursuing
            && Vector3.Distance(transform.position, _currentPositionTarget) < _attackDistance)
        {
            _state = UnitState.Fighting;
        }
        // Если в атаке отошли далеко, возвращаемся к преследованию
        else if (_state == UnitState.Fighting
            && Vector3.Distance(transform.position, _currentPositionTarget) > _attackDistance)
        {
            _state = UnitState.Pursuing;
        }
        // Если еще не дошли до центра или отошли от него далеко
        else if (_state == UnitState.Wandering
                 && Vector3.Distance(transform.position, _currentPositionTarget) > _seekingDistance)
        {
            _state = UnitState.Seeking;
            _currentPositionTarget = _defaultTarget.position;
        }
        // Если дошли до центра (или любой другой желанной точки)
        else if (_state == UnitState.Seeking
                 && Vector3.Distance(transform.position, _currentPositionTarget) < _seekingDistance)
        {
            _state = UnitState.Wandering;
        }
    }

    private void UpdateFightingBehavior()
    {
        if (_state == UnitState.Dead)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
        else if (_state == UnitState.Fighting)
        {
            var damage = _unitSettings.CalculateDamage();
            var enemyIsAlive = _currentUnitTarget.DamageAndCheckIfAlive(damage);
            if (!enemyIsAlive)
            {
                _state = UnitState.Seeking;
                _currentPositionTarget = _defaultTarget.position;
            }
        }
    }

    private void UpdateMovingBehavior()
    {
        if (_state is UnitState.Fighting or UnitState.Dead)
        {
            return;
        }
        
        transform.LookAt(_currentPositionTarget);

        Vector3 steering;
        switch (_state)
        {
            case UnitState.Pursuing:
            case UnitState.Seeking:
                steering = SteeringBehavior.Seek(transform.position, _currentPositionTarget, _unitSettings.speed);
                break;
            case UnitState.Wandering:
                steering = SteeringBehavior.Wander(transform.position, _unitSettings.speed);
                break;
            case UnitState.Fighting:
            case UnitState.Dead:
            default:
                throw new ArgumentOutOfRangeException();
        }

        transform.position += steering * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Если уже завязались или умерли, не переключаемся
        if (_state is UnitState.Pursuing or UnitState.Fighting or UnitState.Dead)
        {
            return;
        }
        
        // Если в сферу с триггером вошел юнит другого цвета, переключаемся на него
        if (other.TryGetComponent(out Unit anotherUnit) && anotherUnit.GetUnitType() != GetUnitType())
        {
            _state = UnitState.Pursuing;
            _currentUnitTarget = anotherUnit;
            _currentPositionTarget = anotherUnit.transform.position;
        }
    }

    private bool DamageAndCheckIfAlive(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _state = UnitState.Dead;
            return false;
        }

        return true;
    }

    private UnitType GetUnitType() => _unitSettings.unitType;

    public static Unit Spawn(UnitSettings unitSettings, Material material,
        Transform spawnPoint, Transform defaultTarget)
    {
        var unit = Instantiate(unitSettings.unitPrefab, spawnPoint.position, spawnPoint.rotation)
            .GetComponent<Unit>();
        
        unit.Init(material, unitSettings, defaultTarget);
        
        return unit;
    }

    private void Init(Material material,
        UnitSettings unitSettings, 
        Transform defaultTarget)
    {
        GetComponent<MeshRenderer>().sharedMaterial = material;

        _defaultTarget = defaultTarget;
        _unitSettings = unitSettings;
        _health = unitSettings.maxHealth;
    }
}