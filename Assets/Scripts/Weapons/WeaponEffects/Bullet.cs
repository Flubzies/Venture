using ObjectPooling;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (TrailRenderer))]
public class Bullet : PoolObject
{
	[SerializeField] protected int _bulletSpeed = 5;

	protected Rigidbody2D _rb;
	protected TrailRenderer _tr;

	protected void Awake ()
	{
		_rb = GetComponent<Rigidbody2D> ();
		_tr = GetComponent<TrailRenderer> ();
	}

	public override void OnObjectReuse ()
	{
		_tr.Clear ();
	}

	public void SetVelocity (Vector2 vel_)
	{
		_rb.velocity = vel_ * _bulletSpeed;
	}

}