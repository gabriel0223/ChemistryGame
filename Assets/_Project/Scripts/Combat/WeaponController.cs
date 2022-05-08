using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : Equipment
{
    public new virtual int GetPower()
    {
        float randomDamage = Random.Range(_power - (_power * 0.1f), _power + (_power * 0.1f));
        return (int)randomDamage;
    }
}
