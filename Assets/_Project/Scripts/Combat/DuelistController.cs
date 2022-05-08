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

    private void Awake()
    {
        _weapon = GetComponent<WeaponController>();
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

    private void Defend()
    {
        
    }

    private void TakeDamage(int damage)
    {
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
