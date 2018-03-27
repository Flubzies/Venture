using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManagerClasses
{
	public class MainMenu : MonoBehaviour
	{
		private void Start ()
		{
			GetComponent<Canvas> ().worldCamera = Camera.main;
		}

		public void OnClickStart ()
		{
			ApplicationManager.instance.LoadLevel01 ();
		}

		public void OnClickExit ()
		{
			ApplicationManager.instance.LoadExit ();
		}

		public void Settings ()
		{
			SettingsMenu.instance.ToggleSettings (true);
		}
	}

}