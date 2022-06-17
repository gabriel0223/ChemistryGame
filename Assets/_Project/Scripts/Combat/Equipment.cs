using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public event Action<int> OnChangeDurability;
    public event Action OnEquipmentBreak;
    
    [SerializeField] protected int _power;
    [SerializeField] protected int _durability;

    private int _basePower;
    private bool _hasInitialized;

    public int Power => _power;

    private void Start()
    {
        if (_hasInitialized)
        {
            return;
        }
        
        _basePower = _power;
    }

    public void Initialize(int power)
    {
        _power = power;
        _basePower = power;
        _hasInitialized = true;
    }

    public int GetPower()
    {
        return _power;
    }

    public void AddPower(int power)
    {
        _power += power;
    }

    public void MultiplyPower(float multiplier)
    {
        int newPower = Mathf.RoundToInt(_power * multiplier);
        _power = newPower;
    }

    public void DecreaseDurability()
    {
        _durability--;

        if (_durability == 0)
        {
            Break();
        }
        
        OnChangeDurability?.Invoke(_durability);
    }

    private void Break()
    {
        //reset
        _power = _basePower;
        _durability = 3;
        OnEquipmentBreak?.Invoke();
    }
}
