using UnityEngine;

namespace Utils
{
    public static class RandomExtensions
    {
        public static bool ChancesGood(this float baseChanceValue) => Random.Range(0, 1) <= baseChanceValue / 100;
    }
}