using System;
using Enums;
using UnityEngine;
using Utils;

namespace Configuration
{
    [CreateAssetMenu]
    public class UnitSettings : ScriptableObject
    {
        [Header("Main Stats")] 
        [Tooltip("Максимальное здоровье"), ShowInSettings("Максимальное здоровье")] public int maxHealth;
        [Tooltip("Скорость передвижения"), ShowInSettings("Скорость передвижения")] public float speed;
        
        [Header("Attacks")] 
        [Tooltip("Шанс промаха"), Range(1, 100), ShowInSettings("Шанс промаха")] public float missChance;
        [Tooltip("Шанс удвоенного урона"), Range(1, 100), ShowInSettings("Шанс удвоенного урона")] public float critChance;
        [Tooltip("Быстрая/Слабая атака"), ShowInSettings("Быстрая/Слабая атака")] public AttackSettings quickAttack;
        [Tooltip("Медленная/Сильная атака"), ShowInSettings("Медленная/Сильная атака")] public AttackSettings strongAttack;
        
        [SerializeField] public Transform unitPrefab;
        [SerializeField] public UnitType unitType;
    }

    [Serializable]
    public struct AttackSettings
    {
        [Tooltip("Урон"), ShowInSettings("Урон")] public int damage;
        [Tooltip("Шанс использования"), Range(1, 100), ShowInSettings("Шанс использования")] public float chanceToUse;
    }

    public static class AttackExtensions
    {
        public static int CalculateDamage(this UnitSettings unit)
        {
            var attackIsMissing = unit.missChance.ChancesGood();
            var attackIsCrit = unit.critChance.ChancesGood();

            var damageFactor = attackIsCrit
                ? 2
                : attackIsMissing
                    ? 0
                    : 1;
            
            var attackIsStrong = unit.strongAttack.chanceToUse.ChancesGood();

            var damage = (attackIsStrong
                ? unit.strongAttack.damage
                : unit.quickAttack.damage) * damageFactor;

            return damage;
        }
    }
}