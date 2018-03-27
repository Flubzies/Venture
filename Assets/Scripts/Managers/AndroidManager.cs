using UnityEngine;

namespace ManagerClasses
{
	// I wanted some Haptic Feedback and Handheld.Vibrate is absolutely awful so I got this script online.
	// https://forum.unity.com/threads/guide-haptic-feedback-on-android-with-no-plugin.382384/
	public class AndroidManager : MonoBehaviour
	{
		private class HapticFeedbackManager
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			private int HapticFeedbackConstantsKey;
			private AndroidJavaObject UnityPlayer;
#endif

			public HapticFeedbackManager ()
			{
#if UNITY_ANDROID && !UNITY_EDITOR
				HapticFeedbackConstantsKey = new AndroidJavaClass ("android.view.HapticFeedbackConstants").GetStatic<int> ("VIRTUAL_KEY");
				UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject> ("currentActivity").Get<AndroidJavaObject> ("mUnityPlayer");
#endif
			}

			public bool Execute ()
			{
				if (SettingsMenu.instance._hapticFeedback)
				{
#if UNITY_ANDROID && !UNITY_EDITOR
					return UnityPlayer.Call<bool> ("performHapticFeedback", HapticFeedbackConstantsKey);
#else
					return false;
#endif
				}
				else
					return false;
			}
		}

		//Cache the Manager for performance
		private static HapticFeedbackManager mHapticFeedbackManager;

		public static bool HapticFeedback ()
		{
			if (mHapticFeedbackManager == null)
			{
				mHapticFeedbackManager = new HapticFeedbackManager ();
			}
			return mHapticFeedbackManager.Execute ();
		}
	}
}