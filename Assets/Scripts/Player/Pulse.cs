using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CanvasGroup))]
public class Pulse : MonoBehaviour
{
	CanvasGroup _cg;
	[SerializeField] float _fadeDuration = 0.2f;

	void OnEnable ()
	{
		_cg = GetComponent<CanvasGroup> ();
		_cg.blocksRaycasts = false;
		_cg.alpha = 0;
		StartCoroutine (PulseBeam ());
	}

	IEnumerator PulseBeam ()
	{
		while (true)
		{
			StartCoroutine (_cg.FadeInCG (_fadeDuration, true));
			Debug.Log ("ASD");
			yield return new WaitForSecondsRealtime (_fadeDuration);
			Debug.Log ("ASDQWE");
			StartCoroutine (_cg.FadeOutCG (_fadeDuration, true));
		}
	}

}