using System;
using UnityEngine;

// I just thought a Health Class would be a good idea. I've tried to make it as independant 
// and easy to use/understand as possible so that I can easily reuse it for other gameobjects  
// like enemies and structures.
public class Health : MonoBehaviour
{
	int _health;
	bool _isDead;

	[SerializeField]
	[Range (1, 1000)]
	[Tooltip ("The Initial Health of the GameObject.")]
	private int _initialHealth;

	[SerializeField]
	[Range (1, 1000)]
	[Tooltip ("The Maximum Health of the GameObject.")]
	private int _maxHealth;
	public int _GetMaxHealth { get { return _maxHealth; } }

	[SerializeField]
	[Tooltip ("Show Health Debug Logs in console.")]
	private bool _debug;

	[SerializeField]
	[Tooltip ("Prevents damage to GameObject.")]
	private bool _invincible;
	public bool _Invincible { get { return _invincible; } set { _invincible = value; } }

	/// <summary>
	/// When the health reaches 0 any methods that are subscribed will be called.
	/// </summary>
	public event Action DeathEvent;

	/// <summary>
	/// Called when the GameObject is damaged.
	/// </summary>
	public event Action DamagedEvent;

	public float _GetHealthPercent
	{
		get
		{
			if (_maxHealth != 0) return (float) _health / (float) _maxHealth;
			else Debug.LogError ("Max Health is 0!");
			return 0;
		}
	}

	void Start ()
	{
		Reset ();
	}

	/// <summary>
	/// Adds to the health.
	/// </summary>
	public void Heal (int _healValue)
	{
		_health += Mathf.Abs (_healValue);
		if (_health > _maxHealth) _health = _maxHealth;
		if (_debug) Debug.Log (gameObject.name + " healed: " + _healValue + " | Health: " + _health);
	}

	/// <summary>
	/// Takes away from the health.
	/// </summary>
	public void Damage (int _damageValue)
	{
		if (_invincible) return;
		_health -= Mathf.Abs (_damageValue);
		if (_health < 0) _health = 0;
		if (_debug) Debug.Log (gameObject.name + " damaged: " + _damageValue + " | Health: " + _health);
		if (DamagedEvent != null) DamagedEvent ();
		if (IsDead () && DeathEvent != null) DeathEvent ();
	}

	/// <summary>
	/// Returns true if the health is less than or equal to 0.
	/// </summary>
	public bool IsDead ()
	{
		if (_health <= 0) _isDead = true;
		return _isDead;
	}

	/// <summary>
	/// Resets the health component to its orignal values.
	/// </summary>
	public void Reset ()
	{
		_isDead = false;
		_health = _initialHealth;
	}

	private void OnValidate ()
	{
		if (_initialHealth <= 0)
			_initialHealth = 1;

		if (_maxHealth < _initialHealth)
			_maxHealth = _initialHealth;
	}

}