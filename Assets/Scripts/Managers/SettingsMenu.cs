using System.Collections;
using System.Collections.Generic;
using PlayerClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ManagerClasses
{
	// Wanted to add Post Process options, but that'd take too long.
	// This class handles all the UI within the Settings Canvas 
	// and how they relate with other gameobjects within the game.
	// Other classes can then easily check this singleton to 
	// use the proper settings.
	public class SettingsMenu : MonoBehaviour
	{
		CanvasGroup _cg;
		bool _menuOpen;
		public bool _hapticFeedback = true;

		[SerializeField] Button _restartButton;
		[SerializeField] Button _mainMenuButton;
		[SerializeField] Button _recalibrateButton;
		[SerializeField] Slider _slider;

		static SettingsMenu _instance;
		public static SettingsMenu instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<SettingsMenu> ();
				return _instance;
			}
		}

		private void Awake ()
		{
			_cg = GetComponent<CanvasGroup> ();
			SceneManager.sceneLoaded += ButtonCheck;
		}

		private void Start ()
		{
			_cg.alpha = 0;
			_cg.blocksRaycasts = false;
		}

		void ButtonCheck (Scene scene_, LoadSceneMode mode_)
		{
			if (_restartButton != null) _restartButton.interactable = scene_.name == "Level01" ? true : false;
			if (_mainMenuButton != null) _mainMenuButton.interactable = scene_.name == "MainMenu" ? false : true;
			if (_recalibrateButton != null) _recalibrateButton.interactable = scene_.name == "MainMenu" ? false : true;
		}

		public void ToggleHapticFeedback (bool toggle_)
		{
			_hapticFeedback = toggle_;
		}

		public void Recalibrate ()
		{
			PlayerMovement p = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
			if (p)
			{
				ToggleSettings (false);
				p.RecalibrationCanvas ();
			}
			else Debug.LogError ("Player not found!");
		}

		public void OnVolumeSliderChanged (float sliderValue_)
		{
			AudioListener.volume = sliderValue_;
		}

		public void ToggleSettings (bool openMenu_)
		{
			if (openMenu_ && !_menuOpen)
			{
				_cg.blocksRaycasts = true;
				Time.timeScale = 0.0f;
				StartCoroutine (_cg.FadeInCG (0.2f, true));
				_menuOpen = true;
			}
			else if (!openMenu_ && _menuOpen)
			{
				StartCoroutine (_cg.FadeOutCG (0.2f, true));
				Time.timeScale = 1;
				_cg.blocksRaycasts = false;
				_menuOpen = false;
			}
		}
	}
}