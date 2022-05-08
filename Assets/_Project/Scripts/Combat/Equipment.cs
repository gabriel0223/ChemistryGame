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

    public int Power => _power;

    public int GetPower()
    {
        return _power;
    }

    public void AddPower(int power)
    {
        _power += power;
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
        _power = 0;
        _durability = 3;
        OnEquipmentBreak?.Invoke();
    }
}
