using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerAttack;
    public event Action OnPlayerDefend;

    [SerializeField] private EquipmentSlot _attackSlot;
    [SerializeField] private EquipmentSlot _defenseSlot;

    private DuelistController _duelistController;
    private WeaponController _weapon;
    private ShieldController _shield;
    private bool _isMyTurn = true;

    private void Awake()
    {
        _duelistController = GetComponent<DuelistController>();
        _weapon = GetComponent<WeaponController>();
        _shield = GetComponent<ShieldController>();
    }

    public void Attack()
    {
        if (!_isMyTurn || _weapon.Power == 0)
        {
            return;
        }
        
        _attackSlot.ActivateSlot();
        OnPlayerAttack?.Invoke();
        
        SetPlayerTurn(false);
    }
    
    public void Defend()
    {
        if (!_isMyTurn || _shield.Power == 0)
        {
            return;
        }
        
        _defenseSlot.ActivateSlot();
        OnPlayerDefend?.Invoke();
        
        SetPlayerTurn(false);
    }

    public void SetPlayerTurn(bool value)
    {
        _isMyTurn = value;
    }
}
