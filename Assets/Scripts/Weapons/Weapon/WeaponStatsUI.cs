using System.Collections;
using System.Collections.Generic;
using PlayerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace WeaponClasses
{
	[RequireComponent (typeof (CanvasGroup))]
	public class WeaponStatsUI : MonoBehaviour
	{
		[Header ("Weapon PickUp:")]
		[SerializeField] Image _weaponImage;
		[SerializeField] Text _weaponName;
		[SerializeField] Text _weaponRarity;
		[SerializeField] Text _damage;
		[SerializeField] Text _range;
		[SerializeField] Text _fireRate;
		[SerializeField] Image _panel;

		CanvasGroup _cg;

		Weapon _weapontemp;
		PlayerWeapon _playerWeapon;

		public bool _MenuOpen { get; private set; }

		static WeaponStatsUI _instance;

		public static WeaponStatsUI instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<WeaponStatsUI> ();
				return _instance;
			}
		}

		void Awake ()
		{
			_cg = GetComponent<CanvasGroup> ();
		}

		void Start ()
		{
			_cg.alpha = 0;
		}

		public void ShowStats (Weapon w_, PlayerWeapon p_ = null)
		{
			StartCoroutine (_cg.FadeInCG (0.2f));
			WeaponSO weaponSO = w_.GetWeaponSO ();

			_weaponImage.sprite = weaponSO._sprite;
			_weaponImage.color = _panel.color = weaponSO._rarity._rarityColor;
			_weaponName.text = weaponSO._name.ToUpper ();
			_weaponRarity.text = weaponSO._rarity._rarityName.ToString ().ToUpper ();
			_damage.text = weaponSO._damage.ToString ();
			_range.text = weaponSO._range.ToString ("0.0");
			_fireRate.text = weaponSO._fireRate.ToString ("0.0");

			Time.timeScale = 0.2f;

			_weapontemp = w_;
			if (p_ != null) _playerWeapon = p_;
			_MenuOpen = true;
		}

		public void HideStats ()
		{
			StartCoroutine (_cg.FadeOutCG (0.2f));
			Time.timeScale = 1f;
			_MenuOpen = false;
		}

		public void OnClickAttachWeapon ()
		{
			if (_weapontemp != null && _playerWeapon != null && _weapontemp != _playerWeapon._GetWeapon)
			{
				_playerWeapon.AttachWeapon (_weapontemp);
			}
		}
	}
}