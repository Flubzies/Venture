using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RarityClasses
{
	public class RarityStats : MonoBehaviour
	{
		public List<RarityType> _rarityTypes;
		[HideInInspector] public float _float = 0;

		static RarityStats _instance;
		public static RarityStats instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<RarityStats> ();
				return _instance;
			}
		}

		public RarityType GetRarity ()
		{
			float rand = Random.Range (0.0f, 1.0f);
			RarityType _rarityToReturn = _rarityTypes[0];

			foreach (RarityType rt in _rarityTypes)
			{
				if (rand <= rt._chanceToSpawn)
					_rarityToReturn = rt;
			}
			return _rarityToReturn;
		}
	}
}