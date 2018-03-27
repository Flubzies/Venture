using ManagerClasses;
using UnityEngine;

public class Portal : MonoBehaviour
{

	CanvasGroup _cg;

	private void Awake ()
	{
		_cg = GetComponentInChildren<CanvasGroup> ();
	}

	private void Start ()
	{
		_cg.blocksRaycasts = false;
		_cg.alpha = 0;
	}

	public void NextLevel ()
	{
		LevelManager.instance.LoadNextLevel ();
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		StartCoroutine (_cg.FadeInCG ());
		_cg.blocksRaycasts = true;
	}

	private void OnTriggerExit2D (Collider2D other)
	{
		StartCoroutine (_cg.FadeOutCG ());
		_cg.blocksRaycasts = false;
	}

}