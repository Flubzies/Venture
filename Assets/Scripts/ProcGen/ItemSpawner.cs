using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProcGen
{
	[System.Serializable]
	public class ItemSpawner : MonoBehaviour
	{
		[Space (1)][Header ("Spawn Rate")]

		[SerializeField] int _minSpawnsPerRoom;
		[SerializeField] int _maxSpawnsPerRoom;

		MapGenerator _mapGen;
		List<Transform> _spawnedObjects;
		List<Vector2> _objectLocations;

		[Space (1)][Header ("Mandatory Spawns")]

		[SerializeField] Transform _player;
		[SerializeField] Transform _portal;

		[Space (1)][Header ("Item Spawns")]

		public List<Transform> _objectsToSpawn;
		[SerializeField] Transform _spawnContainer;
		[HideInInspector, SerializeField] public List<float> _spawnChances;

		static ItemSpawner _instance;
		public static ItemSpawner instance
		{
			get
			{
				if (!_instance)
					_instance = FindObjectOfType<ItemSpawner> ();
				return _instance;
			}
		}

		void Awake ()
		{
			_mapGen = GetComponent<MapGenerator> ();
			_spawnedObjects = new List<Transform> ();
			_objectLocations = new List<Vector2> ();
		}

		public void SpawnObjects ()
		{
			ClearObjects ();
			_objectLocations = _mapGen.GetSpawnLocations (_minSpawnsPerRoom, _maxSpawnsPerRoom);

			_player = Instantiate (_player, Vector2.zero, Quaternion.identity);
			_portal = Instantiate (_portal, Vector2.zero, Quaternion.identity);

			int randA = 0;
			int randB = 0;

			while (randA == randB)
			{
				randA = Random.Range (0, _objectLocations.Count);
				randB = Random.Range (0, _objectLocations.Count);
			}

			_player.position = _objectLocations[randA];
			_portal.position = _objectLocations[randB];

			for (int i = 0; i < _objectLocations.Count; i++)
			{
				if (i == randA || i == randB) continue;
				Transform t = Instantiate (GetObjectToSpawn (_objectsToSpawn), _objectLocations[i], Quaternion.identity, _spawnContainer);
				_spawnedObjects.Add (t);
			}
		}

		public void RemoveObject (Transform objectToRemove_)
		{
			_spawnedObjects.Remove (objectToRemove_);
		}

		public void AddObject (Transform objectToAdd_)
		{
			_spawnedObjects.Add (objectToAdd_);
			objectToAdd_.parent = _spawnContainer;
		}

		Transform GetObjectToSpawn (List<Transform> transList_)
		{
			float rand = Random.Range (0.0f, 1.0f);
			Transform _objectToReturn = transList_[0];

			for (int i = 0; i < transList_.Count; i++)
			{
				if (rand <= _spawnChances[i])
					_objectToReturn = transList_[i];
			}

			return _objectToReturn;
		}

		void ClearObjects ()
		{
			foreach (Transform t in _spawnedObjects)
				if (t != null) Destroy (t.gameObject);

			_spawnedObjects.Clear ();
		}
	}
}