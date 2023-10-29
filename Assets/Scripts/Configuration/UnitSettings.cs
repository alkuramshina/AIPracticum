using System;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu]
    public class UnitSettings : ScriptableObject
    {
        [SerializeField, Tooltip("Максимальное здоровье")] public int maxHealth;
        [SerializeField, Tooltip("Скорость передвижения")] public float speed;
        [SerializeField, Tooltip("Быстрая/Слабая атака")] public Attack quickAttack;
        [SerializeField, Tooltip("Медленная/Сильная атака")] public Attack strongAttack;
        [SerializeField, Tooltip("Шанс промаха")] public float missChance;
        [SerializeField, Tooltip("Шанс удвоенного урона")] public float critChance;
    }


    [Serializable]
    public struct Attack
    {
        [SerializeField, Tooltip("Урон")] public float damage;
        [SerializeField, Tooltip("Шанс использования")] public float chanceToUse;
    }
}