using System.Collections;
using PlayerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace WeaponClasses
{
	public class WeaponPickUp : MonoBehaviour
	{
		[SerializeField] Image _image;
		Weapon _weapon;
		PlayerWeapon _playerWeapon;

		public void SetupPickUpSprite (Weapon w_)
		{
			_weapon = w_;

			_image.sprite = _weapon.GetWeaponSO ()._sprite;
			_image.color = _weapon.GetWeaponSO ()._rarity._rarityColor;

			_playerWeapon = FindObjectOfType<PlayerWeapon> ();
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Player"))
			{
				WeaponStatsUI.instance.ShowStats (_weapon, _playerWeapon);
			}
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.CompareTag ("Player"))
			{
				WeaponStatsUI.instance.HideStats ();
			}
		}
	}
}