using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfig", menuName = "Entity Config/Attack Config")]
public class AttackConfigSO : EventChannelBaseSO
{
	[Tooltip("Character attack strength")]
	[SerializeField] private int _attackStrength;

	[Tooltip("Character attack reload duration (in second).")]
	[SerializeField] private float _attackReloadDuration;

	public int AttackStrength => _attackStrength;
	public float AttackReloadDuration => _attackReloadDuration;
}
