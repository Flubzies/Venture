using System;
using ManagerClasses;
using ProcGen;
using UnityEngine;
using UnityEngine.UI;
using WeaponClasses;

namespace PlayerClasses
{
	public class PlayerWeapon : MonoBehaviour
	{
		[Space (1)]
		[Header ("Starting Weapon:")]
		// TODO: replace with interface.
		[SerializeField] Weapon _weapon;
		public Weapon _GetWeapon { get { return _weapon; } }
		public Transform _firePoint;
		public SpriteRenderer _fireSprite;

		[Space (1)]
		[Header ("Current Weapon:")]
		[SerializeField] Image _weaponImage;
		[SerializeField] Text _weaponText;
		[SerializeField] Text _weaponRarity;
		[Tooltip ("Panel to show Rarity Color.")]
		[SerializeField] Image _panel;

		[Space (1)]
		[Header ("Current Weapon Stats:")]
		[SerializeField] Text _damage;
		[SerializeField] Text _range;
		[SerializeField] Text _fireRate;

		bool _firing;

		void Start ()
		{
			if (LevelManager.instance.GetWeapon () != null)
				AttachWeapon (LevelManager.instance.GetWeapon ());
			else
				AttachWeapon (_weapon);
		}

		void Update ()
		{
			if (_firing && _weapon != null)
				_weapon.WeaponFire ();
		}

		public void StartFiring (bool firing_)
		{
			_firing = firing_;
		}

		public void AttachWeapon (Weapon weapon_)
		{
			// Attaching weapon.
			// Remove current weapon, and send it to item spawner.
			ItemSpawner.instance.AddObject (_weapon.transform);
			_weapon.GetWeaponPickUp ().gameObject.SetActive (true);
			_weapon.transform.position = weapon_.transform.position;
			_weapon.transform.rotation = Quaternion.identity;
			_weapon.transform.localScale = Vector3.one;

			// Remove the new weapon from the item spawnerlist.
			ItemSpawner.instance.RemoveObject (weapon_.transform);

			// Setup the new weapon on the player and player UI.
			_weapon = weapon_;
			WeaponSO weaponSO = _weapon.GetWeaponSO ();

			_weaponImage.sprite = _fireSprite.sprite = weaponSO._sprite;
			_weaponImage.color = _panel.color = weaponSO._rarity._rarityColor;
			_weaponRarity.text = weaponSO._rarity._rarityName.ToUpper ();
			_weaponText.text = weaponSO._name.ToUpper ();

			_weapon.transform.position = Vector2.zero;
			_weapon.transform.parent = _firePoint;
			_weapon.SetFirepoint (_firePoint);
			_weapon.GetWeaponPickUp ().gameObject.SetActive (false);
			ShowCurrentWeaponStats ();
		}

		public void ShowCurrentWeaponStats ()
		{
			WeaponSO weaponSO = _weapon.GetWeaponSO ();
			if (_weapon != null)
			{
				_damage.text = weaponSO._damage.ToString ();
				_range.text = weaponSO._range.ToString ("0.0");
				_fireRate.text = weaponSO._fireRate.ToString ("0.0");
			}
			else
				_damage.text = _range.text = _fireRate.text = "0";
		}
	}
}