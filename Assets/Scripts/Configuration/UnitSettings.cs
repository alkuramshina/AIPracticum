using System;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu]
    public class UnitSettings : ScriptableObject
    {
        [Header("Main Stats")] 
        [Tooltip("Максимальное здоровье")] public int maxHealth;
        [Tooltip("Скорость передвижения")] public float speed;
        
        [Header("Attacks")] 
        [Tooltip("Шанс промаха")] public float missChance;
        [Tooltip("Шанс удвоенного урона")] public float critChance;
        [Tooltip("Быстрая/Слабая атака")] public AttackSettings quickAttack;
        [Tooltip("Медленная/Сильная атака")] public AttackSettings strongAttack;
        
        [SerializeField] public Transform unitPrefab;
    }


    [Serializable]
    public struct AttackSettings
    {
        [Tooltip("Урон")] public float damage;
        [Tooltip("Шанс использования")] public float chanceToUse;
    }
}