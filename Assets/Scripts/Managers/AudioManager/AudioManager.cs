using System;
using ManagerClasses;
using UnityEngine;
using UnityEngine.Audio;

// From Brackeys. Reasoning: I like having a single audio manager as a Singleton to control all my audio. 
// The code isn't anything complicated; it just makes things easier to control for me.
// I added on validate method, since I'm passing strings to make the sound
// I'd like to have less probability of errors. With the onvalidate a string error is very unlikely.
namespace ManagerClasses
{
	public class AudioManager : MonoBehaviour
	{
		public AudioMixerGroup mixerGroup;
		public Sound[] sounds;

		static AudioManager _instance;
		public static AudioManager instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<AudioManager> ();
				return _instance;
			}
		}

		void Awake ()
		{
			foreach (Sound s in sounds)
			{
				s._source = gameObject.AddComponent<AudioSource> ();
				s._source.clip = s._clip;
				s._source.loop = s._loop;

				s._source.outputAudioMixerGroup = mixerGroup;
			}
		}

		public void StopSound (string sound)
		{
			Sound s = Array.Find (sounds, item => item._name == sound);
			if (s == null)
			{
				Debug.LogWarning ("Sound: " + name + " not found!");
				return;
			}
			s._source.Stop ();
			s._source.volume = 0.0f;
		}

		public void Play (string sound)
		{
			Sound s = Array.Find (sounds, item => item._name == sound);
			if (s == null)
			{
				Debug.LogWarning ("Sound: " + name + " not found!");
				return;
			}

			s._source.volume = s._volume * (1f + UnityEngine.Random.Range (-s._volumeVariance / 2f, s._volumeVariance / 2f));
			s._source.pitch = s._pitch * (1f + UnityEngine.Random.Range (-s._pitchVariance / 2f, s._pitchVariance / 2f));

			s._source.Play ();
		}

		private void OnValidate ()
		{
			foreach (Sound sound in sounds)
			{
				if (sound._clip != null) sound._name = sound._clip.name;
			}
		}

	}
}