using System.Collections.Generic;
using UnityEngine;

namespace Configuration
{
	[CreateAssetMenu(menuName = "Animation Configuration")]
	public class AnimationConfiguration : ScriptableObject
	{
		[SerializeField] private AnimationKeyDictionary _keys;

		public IReadOnlyDictionary<AnimationType, string> GetDictionary => _keys.Clone();
	}
}