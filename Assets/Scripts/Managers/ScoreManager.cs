using UnityEngine;
using UnityEngine.UI;

namespace ManagerClasses
{
	[RequireComponent (typeof (CanvasGroup))]
	public class ScoreManager : MonoBehaviour
	{
		[Header ("Score for destroying: ")]
		public int _enemy = 1;
		public int _enemySpawner = 5;

		CanvasGroup _cg;
		bool _menuOpen;

		[HideInInspector] public int _timeSurvived;
		[HideInInspector] public int _enemyKillCount;
		[HideInInspector] public int _score;

		[Header ("Game Over UI: ")]

		[SerializeField] Text _timeSurvivedText;
		[SerializeField] Text _killCountText;
		[SerializeField] Text _scoreText;

		static ScoreManager _instance;
		public static ScoreManager instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<ScoreManager> ();
				return _instance;
			}
		}

		private void Awake ()
		{
			_cg = GetComponent<CanvasGroup> ();
		}

		private void Start ()
		{
			_cg.blocksRaycasts = false;
			_cg.alpha = 0;
		}

		public void GameOver ()
		{
			// _timeSurvivedText.text = _timeSurvived.ToString ("0.0") + "MINUTES";
			_timeSurvivedText.text = "TODO!";
			_killCountText.text = _enemyKillCount.ToString ();
			_scoreText.text = _score.ToString ();
			GameOverMenu (true);
		}

		public void GameOverMenu (bool openMenu_)
		{
			if (!_menuOpen && openMenu_)
			{
				_cg.blocksRaycasts = true;
				Time.timeScale = 0;
				StartCoroutine (_cg.FadeInCG (1.2f, true));
				_menuOpen = true;
			}
			else if (!openMenu_ && _menuOpen)
			{
				_cg.blocksRaycasts = false;
				Time.timeScale = 1;
				StartCoroutine (_cg.FadeOutCG (1.2f, true));
				_menuOpen = false;
			}
		}

		public void OnClickRestart ()
		{
			ApplicationManager.instance.ReLoadScene ();
		}

		public void OnClickMainMenu ()
		{
			ApplicationManager.instance.LoadMainMenu ();
		}

		public void OnClickExit ()
		{
			ApplicationManager.instance.LoadExit ();
		}

	}
}