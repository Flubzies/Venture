using UnityEngine;

public class CameraColorSet : MonoBehaviour
{
	public Material _cameraColorMaterial;
	
	void Start ()
	{
		GetComponent<Camera> ().backgroundColor = _cameraColorMaterial.color;
	}
}