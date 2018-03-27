using UnityEngine;

namespace ObjectPooling
{
	// Part of Sebastians PoolCreator Script.
	public class PoolObject : MonoBehaviour
	{
		public virtual void OnObjectReuse () { }
		public virtual void Destroy (float timeUntilDeath_ = 0.0f)
		{
			Invoke ("SetInactive", timeUntilDeath_);
		}

		void SetInactive ()
		{
			gameObject.SetActive (false);
			Debug.Log("Disabling");
		}
	}
}