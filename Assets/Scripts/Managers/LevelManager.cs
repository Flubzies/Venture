using PlayerClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using WeaponClasses;

namespace ManagerClasses
{
	public class LevelManager : MonoBehaviour
	{
		[HideInInspector] Weapon _playerWeapon;
		[HideInInspector] public int _LevelCount;
		[HideInInspector] public int _enemyCounter;

		bool _isLoading = false;

		static LevelManager _instance;
		public static LevelManager instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<LevelManager> ();
				return _instance;
			}
		}

		void Awake ()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			ResetLevelManager ();
		}

		public void LoadNextLevel ()
		{
			if (_isLoading != true)
			{
				AssignValues ();
				ApplicationManager.instance.ReLoadScene ();
				_isLoading = true;
			}
		}

		public Weapon GetWeapon ()
		{
			return GetComponentInChildren<Weapon> ();
		}

		void AssignValues ()
		{
			_playerWeapon = FindObjectOfType<PlayerWeapon> ()._GetWeapon;
			_playerWeapon.transform.parent = transform;
			_LevelCount++;
		}

		void OnSceneLoaded (Scene scene_, LoadSceneMode mode_)
		{
			if (scene_.name != "Level01")
				ResetLevelManager ();
			_isLoading = false;
		}

		public void ResetLevelManager ()
		{
			_playerWeapon = null;
			_LevelCount = 0;
			_enemyCounter = 5;
		}
	}
}