using System.Collections;
using System.Collections.Generic;
using ManagerClasses;
using ObjectPooling;
using UnityEngine;

namespace EnemyClasses
{
	public class EnemySpawner : MonoBehaviour
	{
		public float _enemySpawnRate;

		Health _health;

		private void Awake ()
		{
			_health = GetComponent<Health> ();
		}

		void Start ()
		{
			_health.DeathEvent += OnDeath;
			StartCoroutine (SpawnEnemy ());
		}

		IEnumerator SpawnEnemy ()
		{
			while (true)
			{
				yield return new WaitForSeconds (_enemySpawnRate);
				PoolManager.instance.ReuseEnemyPool (transform.position, Quaternion.identity);
				EnemyManager.instance._enemyCount++;
			}
		}

		void OnDeath ()
		{
			ScoreManager.instance._enemyKillCount++;
			ScoreManager.instance._score += ScoreManager.instance._enemySpawner;
			PoolManager.instance.ReuseExplosionEffect (transform.position, transform.rotation);

			gameObject.SetActive (false);
		}

	}
}