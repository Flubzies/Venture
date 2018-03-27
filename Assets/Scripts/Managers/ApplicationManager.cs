using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Singleton that manages the application. Primarily for scene management.
namespace ManagerClasses
{
	public class ApplicationManager : MonoBehaviour
	{
		static SceneFader _sceneFader;

		public static ApplicationManager instance = null;
		void Awake ()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy (gameObject);

			DontDestroyOnLoad (gameObject);
		}

		private void Start ()
		{
			_sceneFader = GetComponentInChildren<SceneFader> ();
			SceneManager.sceneLoaded += OnSceneLoaded;
			AudioManager.instance.Play ("MainTheme");
		}

		private void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Escape))
				SettingsMenu.instance.ToggleSettings (true);
		}

		void OnSceneLoaded (Scene scene_, LoadSceneMode mode_)
		{
			_sceneFader.FadeFromScene ();
			if (EnemyManager.instance != null) Debug.Log (EnemyManager.instance._enemyCount);
		}

		public void LoadMainMenu ()
		{
			SettingsMenu.instance.ToggleSettings (false);
			_sceneFader.FadeToScene ("MainMenu");
		}

		public void ReLoadScene ()
		{
			SettingsMenu.instance.ToggleSettings (false);
			_sceneFader.FadeToScene (SceneManager.GetActiveScene ().name);
		}

		public void LoadExit ()
		{
			Application.Quit ();
		}

		public void LoadLevel01 ()
		{
			_sceneFader.FadeToScene ("Level01");
		}
	}
}