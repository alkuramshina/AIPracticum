using Configuration;
using Enums;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Transform _defaultTarget;
    private Transform _currentTarget;
    
    private UnitSettings _unitSettings;
    
    private int _health;
    private UnitState _state;
    
    private void Update()
    {
        UpdateUnitState();
        //TryMove();
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
        
        if (Vector3.Distance(transform.position, _currentTarget.position) > .3f)
        {
            transform.position += Vector3.Lerp(transform.position, _currentTarget.position,
                _unitSettings.speed * Time.deltaTime);
        }
    }
    
    public static Unit Spawn(UnitSettings unitSettings, Material material,
        Transform spawnPoint, Transform defaultTarget)
    {
        var unit = Instantiate(unitSettings.unitPrefab).GetComponent<Unit>();
        
        unit.transform.position = spawnPoint.position;
        unit.transform.rotation = spawnPoint.rotation;
        
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
