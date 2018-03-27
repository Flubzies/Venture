using ObjectPooling;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (TrailRenderer))]
public class BulletExplosive : Bullet
{
	int _damage = 1;
	float _radius = 1f;
	LayerMask _layerMask;

	public void SetupBulletExplosive (int damage_, float radius_, LayerMask layerMask_, Vector2 vel_)
	{
		_damage = damage_;
		_radius = radius_;
		_layerMask = layerMask_;
		SetVelocity (vel_);
	}

	void Explode ()
	{
		PoolManager.instance.ReuseExplosionEffect (transform.position, Quaternion.identity);
		Collider2D[] colArray = Physics2D.OverlapCircleAll (transform.position, _radius, _layerMask);
		foreach (Collider2D col in colArray)
			if (col.CompareTag ("Enemy")) col.GetComponent<Health> ().Damage (_damage);
	}

	private void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) return;
		Explode ();
		gameObject.SetActive (false);
	}
}