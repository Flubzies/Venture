using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RarityClasses
{
	[System.Serializable]
	[CreateAssetMenu (fileName = "RarityType", menuName = "RarityType", order = 0)]
	public class RarityType : ScriptableObject
	{
		[Header ("Rarity: ")]
		[Tooltip ("Depending upon the rarity weapon stats are randomly added between the range.")]
		public string _rarityName;
		public Color _rarityColor;
		public float _chanceToSpawn;
		[Space (1)]
		[Header ("Stats: ")]
		[Space (4)]
		[Tooltip ("Minimum additive to weapon stat (Inclusive)")]
		public int _minDamage;
		[Tooltip ("Maximum additive to weapon stat (Exclusive)")]
		public int _maxDamage;
		[Space (4)]
		[Tooltip ("Minimum additive to weapon stat (Inclusive)")]
		public float _minRange;
		[Tooltip ("Maximum additive to weapon stat (Inclusive)")]
		public float _maxRange;
		[Space (4)]
		[Tooltip ("Minimum additive to weapon stat (Inclusive)")]
		public float _minFireRate;
		[Tooltip ("Maximum additive to weapon stat (Inclusive)")]
		public float _maxFireRate;

	}
}