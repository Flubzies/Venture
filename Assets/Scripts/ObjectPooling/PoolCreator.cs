using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script from Sebastian Lague - https://www.youtube.com/watch?v=LhqP3EghQ-Q
namespace ObjectPooling
{
	public class PoolCreator
	{
		Dictionary<int, List<ObjectInstance>> _poolDictionary = new Dictionary<int, List<ObjectInstance>> ();

		public void CreatePool (GameObject prefab_, int poolSize_)
		{
			int poolKey = prefab_.GetInstanceID ();

			if (!_poolDictionary.ContainsKey (poolKey))
			{
				_poolDictionary.Add (poolKey, new List<ObjectInstance> ());

				GameObject poolHolder = new GameObject (prefab_.name + "pool");
				poolHolder.transform.parent = GameObject.FindGameObjectWithTag ("PoolManager").transform;

				for (int i = 0; i < poolSize_; i++)
				{
					ObjectInstance newObject = new ObjectInstance (MonoBehaviour.Instantiate (prefab_) as GameObject);
					_poolDictionary[poolKey].Add (newObject);
					newObject.SetParent (poolHolder.transform);
				}
			}
		}

		public GameObject ReuseObject (GameObject prefab_, Vector3 position_, Quaternion rotation_)
		{
			int poolKey = prefab_.GetInstanceID ();

			if (_poolDictionary.ContainsKey (poolKey))
			{
				foreach (ObjectInstance ob in _poolDictionary[poolKey])
				{
					if (!ob._gameObject.activeSelf)
					{
						ob.Reuse (position_, rotation_);
						return ob._gameObject;
					}
				}
			}
			return null;
		}

		public class ObjectInstance
		{
			public GameObject _gameObject;
			Transform _transform;

			bool _hasPoolObjectComponent;
			PoolObject _poolObjectScript;

			public ObjectInstance (GameObject objectInstance_)
			{
				_gameObject = objectInstance_;
				_transform = _gameObject.transform;
				_gameObject.SetActive (false);
				_poolObjectScript = _gameObject.GetComponent<PoolObject> ();

				if (_poolObjectScript != null)
					_hasPoolObjectComponent = true;
			}

			public void Reuse (Vector3 position_, Quaternion rotation_)
			{
				_gameObject.SetActive (true);
				_gameObject.transform.position = position_;
				_gameObject.transform.rotation = rotation_;

				if (_hasPoolObjectComponent)
					_poolObjectScript.OnObjectReuse ();
			}

			public void SetParent (Transform parent_)
			{
				_transform.parent = parent_;
			}
		}
	}
}