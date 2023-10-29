using System.Collections.Generic;
using Configuration;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Стартовая цель")]
    private Transform defaultTarget;
    
    [SerializeField, Tooltip("Конфигурация юнитов")]
    private UnitSettings baseUnitSettings;
    
    [SerializeField, Tooltip("Материал юнитов")] 
    private Material baseMaterial;
    
    [SerializeField, Tooltip("Место спауна")] 
    private Transform unitSpawnPoint;
    
    [SerializeField, Tooltip("Время между спаунами, сек.")] 
    private float spawnWaitingTime;
    
    [SerializeField, Tooltip("Максимум юнитов")] 
    private int maxUnitLimit;

    private readonly List<Unit> _spawnedUnits = new();
    private float _spawnTimer;

    private void Update()
    {
        TrySpawn();
    }

    private void TrySpawn()
    {
        if (_spawnedUnits.Count >= maxUnitLimit)
        {
            return;
        }
        
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer < spawnWaitingTime)
        {
            return;
        }
        
        var newUnit = Unit.Spawn(baseUnitSettings, baseMaterial, unitSpawnPoint, defaultTarget);
        _spawnedUnits.Add(newUnit);
        
        _spawnTimer = 0f;
    }

    private void ChangeUnitSettings(UnitSettings newSettings)
    {
        baseUnitSettings.speed = newSettings.speed;
        baseUnitSettings.maxHealth = newSettings.maxHealth;
        baseUnitSettings.critChance = newSettings.critChance;
        baseUnitSettings.missChance = newSettings.missChance;
        baseUnitSettings.quickAttack = newSettings.quickAttack;
        baseUnitSettings.strongAttack = newSettings.strongAttack;
    }
}
