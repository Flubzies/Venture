using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// I've added a subscription to onSceneLoad to Fade into the new scene automatically.
// Added loading bar functionality to the same script.
// Added to Managers namespace.
// Made it look more readable in the inspector.
// Integrated with App Man - DDOL Singleton because I want this transition for every scene.
namespace ManagerClasses
{
    public class SceneFader : MonoBehaviour
    {

        [Header ("Scene Fader")]
        CanvasGroup _cg;
        [SerializeField] float _fadeDuration = 1.0f;

        [Space (2)]

        [Header ("Loading")]
        [SerializeField] GameObject _loadingPanel;
        [SerializeField] Image _loadingAmount;

        void Awake ()
        {
            _cg = GetComponent<CanvasGroup> ();
        }

        private void Start ()
        {
            _cg.alpha = 1;
            _loadingPanel.SetActive (false);
            FadeFromScene ();
        }

        public void FadeFromScene ()
        {
            StartCoroutine (_cg.FadeOutCG (_fadeDuration, true));
        }

        public void FadeToScene (string scene_ = null)
        {
            StartCoroutine (_cg.FadeInCG (_fadeDuration, true));
            if (scene_ != null) StartCoroutine (LoadAsync (scene_));
        }
 
        IEnumerator LoadAsync (string scene_)
        {
            yield return new WaitForSecondsRealtime (_fadeDuration);

            _loadingPanel.SetActive (true);

            AsyncOperation operation = SceneManager.LoadSceneAsync (scene_);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01 (operation.progress / 0.9f);
                _loadingAmount.fillAmount = progress;
                yield return null;
            }

            _loadingPanel.SetActive (false);
        }
    }
}