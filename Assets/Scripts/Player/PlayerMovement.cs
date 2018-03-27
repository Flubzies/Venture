using System;
using System.Collections;
using ManagerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerClasses
{
    public interface IPlayerMovement
    {
        Transform GetTransform ();
    }

    public class PlayerMovement : MonoBehaviour, IPlayerMovement
    {
        Rigidbody2D _playerRB;
        [SerializeField] float _maximumVelocity = 2.0f;
        [SerializeField] float _accelerationSensitivity = 2.0f;

        PlayerStats _playerStats;

        [Space (1)]
        [Header ("Accelerometer: ")]
        CalibratedAccelerometer _acceler;
        [SerializeField] float _deadZone = 0.168f;
        [SerializeField] Text _calibrationText;
        [SerializeField] CanvasGroup _canvasGroup;

        bool _menuOpen;
        bool _stopMovement;
        [HideInInspector] public bool _GetMovement { get { return !_stopMovement; } }

        void Awake ()
        {
            _playerRB = GetComponent<Rigidbody2D> ();
            _playerStats = GetComponentInParent<PlayerStats> ();
            _acceler = new CalibratedAccelerometer ();
        }

        void Start ()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            RecalibrationCanvas ();
        }

        public void RecalibrationCanvas (bool closeMenu = false)
        {
            _calibrationText.text = "START";
            if (!_menuOpen && !closeMenu)
            {
                StartCoroutine (_canvasGroup.FadeInCG (0.4f, true));
                _canvasGroup.blocksRaycasts = true;
                _menuOpen = true;
                Time.timeScale = 0;
            }
            else
            {
                StartCoroutine (CalibratingText ());
                _canvasGroup.blocksRaycasts = false;
                _menuOpen = false;
                Time.timeScale = 1;
                _acceler.CalibrateAccelerometer ();
            }
        }

        IEnumerator CalibratingText ()
        {
            float _time = 0.2f;
            yield return new WaitForSeconds (_time);
            _calibrationText.text = "CALIBRATING.";
            yield return new WaitForSeconds (_time);
            _calibrationText.text = "CALIBRATING..";
            yield return new WaitForSeconds (_time);
            _calibrationText.text = "CALIBRATING...";
            yield return new WaitForSeconds (_time);
            StartCoroutine (_canvasGroup.FadeOutCG (0.4f, true));
        }

        public Transform GetTransform ()
        {
            return transform;
        }

        void FixedUpdate ()
        {
            // UNITY_ANDROID and EDITOR Tags weren't playing well with Unity Remote 
            // so I just decided to use Remote for all debugging instead.

            // #if UNITY_ANDROID
            AccelerometerMovement ();
            // #endif
            // #if UNITY_EDITOR
            //             StopPlayerMovement (Input.GetKey (KeyCode.LeftShift));
            //             PCMovement ();
            // #endif
            // ControllerMovement ();
        }

        void AccelerometerMovement ()
        {
            Vector2 acceleration = _acceler.GetAccelerometer (Input.acceleration);
            // Gets a calibrated accelerometer. (Allows for the phone deadzone to be offset.)
            if (Math.Abs (acceleration.x) <= _deadZone && Math.Abs (acceleration.y) <= _deadZone) return;
            // acceleration multiplied to make movement more sensitive.
            Vector2 movement = new Vector2 (_accelerationSensitivity * acceleration.x, _accelerationSensitivity * acceleration.y);
            // Adding a deadzone to the movement so the player will not move rapidly while the phone is lying flat.
            if (!_stopMovement) _playerRB.velocity = Vector2.ClampMagnitude (movement * _playerStats._speed, _maximumVelocity);
            // Stops if the stop button is pressed. We do this here beacause we still want the velocity for rotations.
            // Velocity clamp to make sure the player won't go too fast.
            transform.RotateTowardsVector (acceleration, _playerStats._rotationSpeed);
            // Extension Method to rotate the player towards their velocity.
        }

        // Used for debugging.
        void PCMovement ()
        {
            Vector2 movement = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
            if (!_stopMovement) _playerRB.velocity = movement * _playerStats._speed;
            transform.RotateTowardsVector (movement, _playerStats._rotationSpeed);
        }

        public void StopPlayerMovement (bool stopMovement)
        {
            AndroidManager.HapticFeedback ();
            _playerRB.velocity = Vector2.zero;
            _stopMovement = stopMovement;
        }
    }
}