using ManagerClasses;
using ObjectPooling;
using UnityEngine;

namespace WeaponClasses
{
	public class WeaponLauncher : Weapon
	{
		protected float _fireTimer = 0f;

		public override void WeaponFire ()
		{
			if (Time.time > _fireTimer)
			{
				WeaponLaunch ();
				_fireTimer = (Time.time + (1.0f / _weaponSO._fireRate));
			}
		}

		void WeaponLaunch ()
		{
			AndroidManager.HapticFeedback ();
			AudioManager.instance.Play (_weaponSO._sound);
			PoolManager.instance.ReuseBulletExplosive (_firepoint.position, Quaternion.identity, _weaponSO._damage, _weaponSO._range / 3.0f, _weaponSO._toHitLayerMask, _firepoint.transform.up);
		}
	}
}