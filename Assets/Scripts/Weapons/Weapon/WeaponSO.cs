using System.Collections.Generic;
using RarityClasses;
using UnityEngine;

namespace WeaponClasses
{
    [CreateAssetMenu (fileName = "Weapon", menuName = "Weapon")]
    public class WeaponSO : ScriptableObject
    {
        [Space (1)]
        [Header ("Weapon Scriptable Object: ")]
        [Tooltip ("Weapon Sprite for pick ups and UI.")]
        public Sprite _sprite;
        public string _name;

        [Space (1)]
        public int _damage;
        public float _range;
        public float _fireRate;

        [Space (1)]
        [Tooltip ("Anything in this Layer Mask will be hit.")]
        public LayerMask _toHitLayerMask;
        [SerializeField] AudioClip _soundEffect;

        [HideInInspector] public string _sound;
        [HideInInspector] public RarityType _rarity;

        private void OnValidate ()
        {
            if (_damage <= 0) _damage = 1;
            if (_fireRate <= 0) _fireRate = 1;
            if (_sound != null) _sound = _soundEffect.name;
        }
    }
}