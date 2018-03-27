using System;
using ManagerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerClasses
{
    public class Player : MonoBehaviour
    {
        [HideInInspector] Health _health;
        [SerializeField] Slider _slider;

        void Awake ()
        {
            _health = GetComponent<Health> ();
        }

        private void Start ()
        {
            _health.Reset ();
            _health.DamagedEvent += DamagedEffect;
            _health.DeathEvent += OnDeath;
            _health.DeathEvent += ScoreManager.instance.GameOver;
        }

        void DamagedEffect ()
        {
            _slider.value = _health._GetHealthPercent;
        }

        void OnDeath ()
        {
            Debug.Log ("TODO: Player Dead!");
        }
    }
}