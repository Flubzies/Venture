using ObjectPooling;
using UnityEngine;

// Didn't have time to finish implementing this yet. 
// Enemies are supposed to get harder the further you go.
namespace ManagerClasses
{
	public class EnemyManager : MonoBehaviour
	{
		static EnemyManager _instance;
		public static EnemyManager instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<EnemyManager> ();
				return _instance;
			}
		}

		public int _initalEnemyCount = 20;
		[HideInInspector] public int _enemyCount;
		[HideInInspector] public int _maxEnemiesInPool;

		private void Awake ()
		{
			_enemyCount = _initalEnemyCount;
			_maxEnemiesInPool = PoolManager.instance._enemiesInPool;
		}
	}
}