using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Integrated myself. Found the conversion maths online.
// https://answers.unity.com/questions/927515/accelerometer-calibration-2.html
namespace PlayerClasses
{
	public class CalibratedAccelerometer
	{
		Matrix4x4 _calibrationMatrix;
		Vector3 _wantedDeadZone = Vector3.zero;

		public Vector3 GetAccelerometer (Vector3 accelerator)
		{
			Vector3 accel = this._calibrationMatrix.MultiplyVector (accelerator);
			return accel;
		}

		public void CalibrateAccelerometer ()
		{
			_wantedDeadZone = Input.acceleration;
			Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0f, 0f, -1f), _wantedDeadZone);
			Matrix4x4 matrix = Matrix4x4.TRS (Vector3.zero, rotateQuaternion, new Vector3 (1f, 1f, 1f));
			_calibrationMatrix = matrix.inverse;
		}
	}
}