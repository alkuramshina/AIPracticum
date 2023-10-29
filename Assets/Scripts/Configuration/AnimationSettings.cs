using System;
using System.Collections.Generic;
using Enums;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(menuName = "Animation Settings")]
    public class AnimationSettings : ScriptableObject
    {
        [SerializeField] private AnimationKeyDictionary keys;

        public IReadOnlyDictionary<AnimationType, string> GetDictionary => keys.Clone();
    }
    
    [Serializable]
    public class AnimationKeyDictionary : SerializableDictionaryBase<AnimationType, string> { }
}