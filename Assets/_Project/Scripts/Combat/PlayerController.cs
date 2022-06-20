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
    private bool _isPlayerTurn = true;
    private bool _inputLocked;

    private void Awake()
    {
        _duelistController = GetComponent<DuelistController>();
        _weapon = GetComponent<WeaponController>();
        _shield = GetComponent<ShieldController>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _duelistController.Initialize("Player", GamePersistentData.Instance.PlayerHealth);
    }

    public void Attack()
    {
        if (!_isPlayerTurn || _weapon.Power == 0 || _inputLocked)
        {
            return;
        }
        
        _attackSlot.ActivateSlot();
        OnPlayerAttack?.Invoke();
        
        EndPlayerTurn();
    }
    
    public void Defend()
    {
        if (!_isPlayerTurn || _shield.Power == 0 || _inputLocked)
        {
            return;
        }
        
        _defenseSlot.ActivateSlot();
        OnPlayerDefend?.Invoke();
        
        EndPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        _isPlayerTurn = true;
    }

    private void EndPlayerTurn()
    {
        _isPlayerTurn = false;
    }

    public void SetInputLocked(bool value)
    {
        _inputLocked = value;
    }
}
