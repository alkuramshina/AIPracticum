using Configuration;
using Enums;
using UnityEngine;

public class Unit : ClickableElement
{
    private Transform _defaultTarget;
    private Transform _currentTarget;
    
    private UnitSettings _unitSettings;
    
    private int _health;
    private UnitState _state;

    private float _attackDistance = 1f;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        UpdateUnitState();
        
        TryMove();
        
        base.Update();
    }

    private void UpdateUnitState()
    {
        // if anyone is around + its attack distance => Fighting
        // if anyone is around => Seeking
        // if at defaultTarget => Idle
        // Wandering
        
        _state = UnitState.Wandering;
        _currentTarget = _defaultTarget;
    }

    private void TryMove()
    {
        if (_state is UnitState.Fighting or UnitState.Idle)
        {
            return;
        }

        transform.LookAt(_currentTarget);
        _characterController.SimpleMove(_currentTarget.position * _unitSettings.speed);
        
        // transform.position += steering * Time.deltaTime;
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