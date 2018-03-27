using ManagerClasses;
using ObjectPooling;
using UnityEngine;

namespace WeaponClasses
{
	public class WeaponRaycast : Weapon
	{
		[Space (1)]
		[Header ("Weapon Raycast: ")]
		[SerializeField] protected BulletTrail _bulletTrail;

		protected float _fireTimer = 0f;
		float raycastRange = 60.0f;

		public override void WeaponFire ()
		{
			if (Time.time > _fireTimer)
			{
				WeaponShoot ();
				_fireTimer = (Time.time + (1.0f / _weaponSO._fireRate));
			}
		}

		void WeaponShoot ()
		{
			RaycastHit2D hit = Physics2D.Raycast (_firepoint.position, _firepoint.transform.up, raycastRange, _weaponSO._toHitLayerMask);

			if (hit.collider != null)
			{
				bool _hitInRange = hit.distance <= _weaponSO._range;

				WeaponEffect (hit.point, _hitInRange);
				Debug.DrawLine (_firepoint.position, hit.point, Color.red);

				if (_hitInRange)
				{
					Health h = hit.collider.GetComponent<Health> ();
					if (h)
					{
						PoolManager.instance.ReuseHitEffectPool (hit.point, Quaternion.identity);
						h.Damage (_weaponSO._damage);
					}
				}
			}
		}

		// I couldn't get the line trail to work with object pooling, so it's not super efficient.
		void WeaponEffect (Vector3 hitPos_, bool hitInRange_)
		{
			AndroidManager.HapticFeedback ();
			AudioManager.instance.Play (_weaponSO._sound);

			BulletTrail trail = Instantiate (_bulletTrail, _firepoint.position, _firepoint.rotation);
			LineRenderer lr = trail.GetComponent<LineRenderer> ();
			if (lr)
			{
				lr.SetPosition (0, _firepoint.position);
				lr.SetPosition (1, hitPos_);
			}
			Destroy (trail.gameObject, 0.1f);
		}
	}
}