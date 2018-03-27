using System.Collections.Generic;
using RarityClasses;
using UnityEngine;

namespace WeaponClasses
{

    public interface IWeapon
    {
        WeaponSO GetWeaponSO ();
        void SetFirepoint (Transform firepoint_);
    }

    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [Header ("Weapon: ")]
        [Tooltip ("Attach the corresponding Weapon Scriptable Object.")]
        [SerializeField] List<WeaponSO> _weaponSOList;
        protected WeaponSO _weaponSO;
        protected Transform _firepoint;
        WeaponPickUp _weaponPickUp;

        void Awake ()
        {
            _weaponPickUp = GetComponentInChildren<WeaponPickUp> ();
            _weaponSO = _weaponSOList.RandomFromList ();
            _weaponSO = Instantiate (_weaponSO);
            SetupSerializableObjectData ();
            if (_weaponPickUp) _weaponPickUp.SetupPickUpSprite (this);
        }

        void SetupSerializableObjectData ()
        {
            RarityType rt = _weaponSO._rarity = RarityStats.instance.GetRarity ();
            _weaponSO._damage = (_weaponSO._damage + UnityEngine.Random.Range (rt._minDamage, rt._maxDamage));
            _weaponSO._range = (_weaponSO._range + UnityEngine.Random.Range (rt._minRange, rt._maxRange));
            _weaponSO._fireRate = (_weaponSO._fireRate + UnityEngine.Random.Range (rt._minFireRate, rt._maxFireRate));
        }

        public WeaponPickUp GetWeaponPickUp ()
        {
            if (_weaponPickUp != null) return _weaponPickUp;
            else return _weaponPickUp = GetComponentInChildren<WeaponPickUp> ();
        }

        public virtual void WeaponFire () { Debug.Log ("Base Weapon Fire Called."); }

        public WeaponSO GetWeaponSO ()
        {
            return _weaponSO;
        }

        public void SetFirepoint (Transform firepoint_)
        {
            _firepoint = firepoint_;
        }

    }
}