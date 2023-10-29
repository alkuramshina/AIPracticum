using System;
using RotaryHeart.Lib.SerializableDictionary;

public enum AnimationType : byte
{
	Move = 0,
	FastAttack = 1,
	StrongAttack = 2,
	Die = 3
}

[Flags]
public enum IgnoreAxisType : byte
{
	None = 0,
	X = 1,
	Y = 2,
	Z = 4
}

[Serializable]
public class AnimationKeyDictionary : SerializableDictionaryBase<AnimationType, string> { }