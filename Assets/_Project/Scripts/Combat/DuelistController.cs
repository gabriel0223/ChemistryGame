using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelistController : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public event Action OnTakeDamage;

    [SerializeField] private LifeBar _lifeBar;
    [SerializeField] private TMP_Text _duelistName;
    [SerializeField] private int _health;

    private WeaponController _weapon;
    private ShieldController _shield;
    private bool _isDefenseActive;

    private void Awake()
    {
        _weapon = GetComponent<WeaponController>();
        _shield = GetComponent<ShieldController>();
    }

    private void OnEnable()
    {
        OnHealthChange += _lifeBar.AnimateBar;
    }
    
    private void OnDisable()
    {
        OnHealthChange -= _lifeBar.AnimateBar;
    }

    private void Start()
    {
        _lifeBar.SetInitialHealth(_health);
    }

    public void Attack(DuelistController targetDuelist)
    {
        targetDuelist.TakeDamage(_weapon.GetPower());
        _weapon.DecreaseDurability();
    }

    public void Defend()
    {
        _isDefenseActive = true;
    }

    private void TakeDamage(int damage)
    {
        if (_isDefenseActive)
        {
            damage -= _shield.GetPower();
            _shield.DecreaseDurability();
            _isDefenseActive = false;
        }

        if (damage <= 0)
        {
            return;
        }
        
        _health -= damage;

        if (_health < 0) { _health = 0; }
        
        OnHealthChange?.Invoke(_health);
        OnTakeDamage?.Invoke();
    }

    public void SetDuelistName(string duelistName)
    {
        _duelistName.SetText(duelistName);
    }
}
