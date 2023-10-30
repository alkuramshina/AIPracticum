using System;
using Configuration;
using Enums;
using UnityEngine;

public class Unit : ClickableElement
{
    private Transform _defaultTarget;
    private Vector3 _currentTarget;
    
    private UnitSettings _unitSettings;
    
    private int _health;
    private UnitState _state;

    private float _attackDistance = 1f;
    private float _seekingDistance = 5f;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        UpdateState();

        if (_state == UnitState.Fighting)
        {
            UpdateFightingBehavior();
        }
        else
        {
            UpdateMovingBehavior();
        }
        
        base.Update();
    }

    private void UpdateState()
    {
        // if anyone is around + its attack distance => Fighting
        // if anyone is around => Seeking
        // if at defaultTarget => Idle
        // Wandering
        
        // Если еще не дошли до центра или отошли от него далеко
        if (_state != UnitState.Wandering
            || Vector3.Distance(transform.position, _currentTarget) >_seekingDistance)
        {
            _state = UnitState.Seeking;
            _currentTarget = _defaultTarget.position;
        }

        // Если дошли до центра (или любой другой желанной точки)
        if (_state == UnitState.Seeking
            && Vector3.Distance(transform.position, _currentTarget) < _seekingDistance)
        {
            _state = UnitState.Wandering;
        }
    }

    private void UpdateFightingBehavior()
    {
        if (_state is not UnitState.Fighting)
        {
            return;
        }
    }

    private void UpdateMovingBehavior()
    {
        if (_state is UnitState.Fighting)
        {
            return;
        }
        
        transform.LookAt(_currentTarget);

        Vector3 steering;
        switch (_state)
        {
            case UnitState.Pursuing:
            case UnitState.Seeking:
                steering = SteeringBehavior.Seek(transform.position, _currentTarget, _unitSettings.speed);
                break;
            case UnitState.Wandering:
                steering = SteeringBehavior.Wander(transform.position, _unitSettings.speed);
                break;
            case UnitState.Fighting:
            default:
                throw new ArgumentOutOfRangeException();
        }

        transform.position += steering * Time.deltaTime;
    }
    
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