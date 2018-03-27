using UnityEngine;

namespace ObjectPooling
{
	public class PoolParticle : PoolObject
	{
		ParticleSystem _particleSystem;

		void Awake ()
		{
			_particleSystem = GetComponent<ParticleSystem> ();
		}

		public override void OnObjectReuse ()
		{
			_particleSystem.Play ();
		}

	}
}