using System.Collections;
using Pathfinding;
using UnityEngine;

// Some more brackeys https://www.youtube.com/watch?v=4T7KHysRw84
// Most of the script is the same.
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour
{
	[HideInInspector] public bool _pathIsEnded = false;

	[SerializeField] float _updateRate = 2f;
	[SerializeField] float _speed = 4f;
	[SerializeField] float _nextWaypointDist = 3f;

	Path _path;
	Seeker _seeker;
	Rigidbody2D _rb;
	Transform _target;

	int _currentWaypoint = 0;
	bool _searchingForPlayer = false;

	void Awake ()
	{
		_seeker = GetComponent<Seeker> ();
		_rb = GetComponent<Rigidbody2D> ();
	}

	void Start ()
	{
		Reset ();
	}

	public void Reset ()
	{
		if (_target == null)
		{
			if (!_searchingForPlayer)
			{
				_searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			return;
		}

		_seeker.StartPath (transform.position, _target.position, OnPathComplete);
		StartCoroutine (UpdatePath ());
	}

	IEnumerator SearchForPlayer ()
	{
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if (sResult == null)
		{
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		}
		else
		{
			_target = sResult.transform;
			_searchingForPlayer = false;
			StartCoroutine (UpdatePath ());
			yield return false;
		}
	}

	IEnumerator UpdatePath ()
	{
		if (_target == null)
		{
			if (!_searchingForPlayer)
			{
				_searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			yield return false;
		}
		else
		{
			_seeker.StartPath (transform.position, _target.position, OnPathComplete);
			yield return new WaitForSeconds (1f / _updateRate);
			StartCoroutine (UpdatePath ());
		}
	}

	public void OnPathComplete (Path p)
	{
		if (p.error) gameObject.SetActive (false);

		if (!p.error)
		{
			_path = p;
			_currentWaypoint = 0;
		}
	}

	void FixedUpdate ()
	{
		if (_target == null)
		{
			if (!_searchingForPlayer)
			{
				_searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			return;
		}

		if (_path == null)
			return;

		if (_currentWaypoint >= _path.vectorPath.Count)
		{
			_pathIsEnded = true;
			return;
		}

		_pathIsEnded = false;

		Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
		_rb.velocity = dir * _speed;
		transform.RotateTowardsVector (_rb.velocity, 4);

		float dist = Vector3.Distance (transform.position, _path.vectorPath[_currentWaypoint]);
		if (dist < _nextWaypointDist)
		{
			_currentWaypoint++;
			return;
		}
	}
}