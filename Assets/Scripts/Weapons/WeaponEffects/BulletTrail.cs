using UnityEngine;

public class BulletTrail : MonoBehaviour
{
	[SerializeField] int moveSpeed = 230;

	void Update ()
	{
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
	}
}