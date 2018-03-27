using UnityEngine;

// learnt off here: https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
public class GestureControls : MonoBehaviour
{
	[SerializeField] float _zoomSpeed = 1.0f;
	[SerializeField] float _minZoom = 8f;
	[SerializeField] float _maxZoom = 12f;
	[SerializeField] float _minimumDeltaMagnitude = 12f;

	Camera _camera;

	void Start ()
	{
		_camera = GetComponent<Camera> ();
	}

	void Update ()
	{
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;

			float prevTouchMag = (touchZeroPrePos - touchOnePrePos).magnitude;
			float touchMag = (touchZero.position - touchOne.position).magnitude;

			float magDiff = touchMag - prevTouchMag;

			// Input only effects camera if the mag size is big enough.

			if (Mathf.Abs (magDiff) < _minimumDeltaMagnitude) return;

			_camera.orthographicSize += (magDiff /= 100f) + _zoomSpeed;
			_camera.orthographicSize = Mathf.Clamp (_camera.orthographicSize, _minZoom, _maxZoom);
		}
	}
}