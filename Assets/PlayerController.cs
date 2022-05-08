using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerAttack;

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
        
        OnPlayerAttack?.Invoke();
        
        SetMyTurn(false);
    }

    public void SetMyTurn(bool value)
    {
        _isMyTurn = value;
    }
}
