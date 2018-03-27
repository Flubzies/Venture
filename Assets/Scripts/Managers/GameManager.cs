using PlayerClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManagerClasses
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager _instance;

		void Awake ()
		{
			if (_instance == null)
				_instance = this;
			else if (_instance != this)
				Destroy (gameObject);
		}

		public static void DestroyPlayer (Player _player, float delay)
		{
			Destroy (_player.gameObject, delay);
		}

		public void Settings ()
		{
			SettingsMenu.instance.ToggleSettings (true);
		}
	}
}