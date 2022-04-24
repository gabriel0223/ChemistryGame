using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public int MoneyObtained { get; private set; }

    public void AddMoney(int money)
    {
        MoneyObtained += money;
    }
}
