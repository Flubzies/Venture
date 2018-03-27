using ManagerClasses;
using ObjectPooling;
using UnityEngine;

namespace EnemyClasses
{
	[RequireComponent (typeof (Health))]
	[RequireComponent (typeof (Seeker))]
	[RequireComponent (typeof (EnemyAI))]
	public class Enemy : PoolObject
	{
		Health _health;
		[SerializeField] protected int _damage;

		private void Awake ()
		{
			_health = GetComponent<Health> ();
		}

		void Start ()
		{
			_health.DeathEvent += OnDeath;
		}

		void OnDeath ()
		{
			ScoreManager.instance._enemyKillCount++;
			ScoreManager.instance._score += ScoreManager.instance._enemy;

			PoolManager.instance.ReuseExplosionEffect (transform.position, Quaternion.identity);
			gameObject.SetActive (false);
		}

		private void OnCollisionEnter2D (Collision2D other)
		{
			if (other.gameObject.CompareTag ("Player"))
			{
				other.gameObject.GetComponent<Health> ().Damage (_damage);
				PoolManager.instance.ReuseHitEffectPool (transform.position, Quaternion.identity);
			}
		}

		public override void OnObjectReuse ()
		{
			_health.Reset ();
			GetComponent<EnemyAI> ().Reset ();
		}
	}
}