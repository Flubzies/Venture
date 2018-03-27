using System.Collections;
using EnemyClasses;
using UnityEngine;

// In the tutorial he has PoolCreator(His PoolManager) as a singleton.
// I decided to make this the PoolManager instead so that I can have multiple pools in this singleton.
// Whereas with his method you could only have one pool in the PoolCreator Singleton.
// I've added functionality where it won't Enque and reuse an active gameobject. 
// This is quite useful because I don't want enemies to randomly disappear if we use all the objects in the pool.
namespace ObjectPooling
{
	public class PoolManager : MonoBehaviour
	{
		[Header ("Hit Effect:")]
		[SerializeField] PoolObject _hitEffect;
		[SerializeField] int _hitEffectsInPoolCount;
		[SerializeField] float _hitEffectTimer;
		public static PoolCreator _HitEffectPool { get; set; }

		[Header ("Explosion Effect:")]
		[SerializeField] PoolObject _explosionEffect;
		[SerializeField] int _explosionEffectsInPoolCount;
		[SerializeField] float _explosionEffectTimer;
		public static PoolCreator _ExplosionEffectPool { get; set; }

		[Header ("Bullet Explosive:")]
		[SerializeField] PoolObject _bulletExplosive;
		[SerializeField] int _bulletExplosivesInPoolCount;
		public static PoolCreator _BulletExplosivePool { get; set; }

		[Space (1)]
		[Header ("Enemy:")]
		[SerializeField] Enemy _enemy;
		[SerializeField] public int _enemiesInPool;
		public static PoolCreator _EnemyPool { get; set; }

		static PoolManager _instance;
		public static PoolManager instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<PoolManager> ();
				return _instance;
			}
		}

		void Awake ()
		{
			_HitEffectPool = new PoolCreator ();
			_HitEffectPool.CreatePool (_hitEffect.gameObject, _hitEffectsInPoolCount);

			_EnemyPool = new PoolCreator ();
			_EnemyPool.CreatePool (_enemy.gameObject, _enemiesInPool);

			_ExplosionEffectPool = new PoolCreator ();
			_ExplosionEffectPool.CreatePool (_explosionEffect.gameObject, _explosionEffectsInPoolCount);

			_BulletExplosivePool = new PoolCreator ();
			_BulletExplosivePool.CreatePool (_bulletExplosive.gameObject, _bulletExplosivesInPoolCount);
		}

		public void ReuseHitEffectPool (Vector3 position_, Quaternion rotation_)
		{
			GameObject g = _HitEffectPool.ReuseObject (_hitEffect.gameObject, position_, rotation_);
			if (g != null) StartCoroutine (SetInactive (g, _hitEffectTimer));
		}

		public void ReuseExplosionEffect (Vector3 position_, Quaternion rotation_)
		{
			GameObject g = _ExplosionEffectPool.ReuseObject (_explosionEffect.gameObject, position_, rotation_);
			if (g != null) StartCoroutine (SetInactive (g, _explosionEffectTimer));
		}

		public void ReuseBulletExplosive (Vector3 position_, Quaternion rotation_, int damage_, float radius_, LayerMask layerMask_, Vector2 vel_)
		{
			GameObject g = _BulletExplosivePool.ReuseObject (_bulletExplosive.gameObject, position_, rotation_);
			if (g != null) g.GetComponent<BulletExplosive> ().SetupBulletExplosive (damage_, radius_, layerMask_, vel_);
		}

		public void ReuseEnemyPool (Vector3 position_, Quaternion rotation_)
		{
			_EnemyPool.ReuseObject (_enemy.gameObject, position_, rotation_);
		}

		IEnumerator SetInactive (GameObject g, float timeBeforeInactive_ = 0.0f)
		{
			yield return new WaitForSeconds (timeBeforeInactive_);
			g.SetActive (false);
		}

	}
}