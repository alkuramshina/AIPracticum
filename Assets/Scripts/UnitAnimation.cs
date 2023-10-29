﻿using System;
using System.Collections.Generic;
using Configuration;
using Enums;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class UnitAnimation : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	[SerializeField] private Collider _collider;

	[Inject] private IReadOnlyDictionary<AnimationType, string> _animationConfiguration;

	/// <summary>
	/// Событие, вызываемое по окончанию анимации
	/// </summary>
	public event EventHandler OnEndAnimation;

	/// <summary>
	/// Этот метод нужно вызвать/подписать на передвижение в Unit, чтобы подключить анимацию стояния или хотьбы
	/// </summary>
	/// <remarks>Если передается 0f - персонаж в Idle анимации, если >0f - персонаж ходит</remarks>
	public void Moving(float direction)
	{
		_animator.SetFloat(_animationConfiguration[AnimationType.Move], direction);
	}

	/// <summary>
	/// Вызывать для всех прочих, кроме хотьбы анимаций
	/// </summary>
	/// <param name="key"></param>
	public void StartAnimation(string key)
	{
		_animator.SetFloat(_animationConfiguration[AnimationType.Move], 0f);
		_animator.SetTrigger(key);
	}

	//Вызывается внутри анимаций для переключения атакующего коллайдера
	private void AnimationEventCollider_UnityEditor(int isActivity)
	{
		_collider.enabled = isActivity != 0;
	}

	//Вызывается внутри анимаций для оповещения об окончании анимации
	private void AnimationEventEnd_UnityEditor(string result)
	{
		//В конце анимации смерти особый аргумент и своя логика обработки
		if (result == "die") Destroy(gameObject);
		OnEndAnimation?.Invoke(this, EventArgs.Empty);
	}
}