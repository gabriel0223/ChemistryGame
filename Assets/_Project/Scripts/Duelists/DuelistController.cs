using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelistController : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public event Action OnTakeDamage;
    public event Action OnEnableDefense;
    public event Action OnDisableDefense;
    public event Action OnDie;

    [SerializeField] private LifeBar _lifeBar;
    [SerializeField] private TMP_Text _duelistName;
    
    private int _health;
    private int _initialHealth;
    private bool _isDefenseActive;
    
    public DuelistController DuelistOpponent { get; private set; }
    public WeaponController Weapon { get; private set; }
    public ShieldController Shield { get; private set; }

    public float HealthPercentage => (float)_health / _initialHealth;

    private void Awake()
    {
        Weapon = GetComponent<WeaponController>();
        Shield = GetComponent<ShieldController>();
    }

    private void OnEnable()
    {
        OnHealthChange += _lifeBar.AnimateBar;
    }
    
    private void OnDisable()
    {
        OnHealthChange -= _lifeBar.AnimateBar;
    }
    
    public void Initialize(string duelistName, int health)
    {
        if (_duelistName != null)
        {
            _duelistName.SetText(duelistName); 
        }
        
        _health = health;
        _initialHealth = _health;
        _lifeBar.SetInitialHealth(_initialHealth);
    }

    public void StartTurn()
    {
        DisableDefense();
    }

    public void Attack(DuelistController targetDuelist)
    {
        targetDuelist.TakeDamage(Weapon.GetPower());
        Weapon.DecreaseDurability();
    }

    public void Defend()
    {
        _isDefenseActive = true;
        OnEnableDefense?.Invoke();
    }

    private void DisableDefense()
    {
        _isDefenseActive = false;
        OnDisableDefense?.Invoke();
    }

    private void TakeDamage(int damage)
    {
        if (_isDefenseActive)
        {
            damage -= Shield.GetPower();
            Shield.DecreaseDurability();
        }

        if (damage <= 0)
        {
            return;
        }
        
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            OnDie?.Invoke();
        }
        
        OnHealthChange?.Invoke(_health);
        OnTakeDamage?.Invoke();
    }

    public void SetOpponent(DuelistController opponent)
    {
        DuelistOpponent = opponent;
    }
}
