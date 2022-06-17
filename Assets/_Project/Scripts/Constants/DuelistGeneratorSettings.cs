using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DuelistGeneratorSettings
{
    private const float InitialHealth = 40f;
    private const float BaseHealth = 10f;
    private const float HealthExponent = 1.75f;
    
    private const float InitialPower = 10f;
    private const float BasePower = 10f;
    private const float PowerExponent = 1.1f;

    private const int InitialUpgradePowerRate = 4;

    public static int GenerateDuelistHealth(int duelistLevel)
    {
        int newHealth = Mathf.RoundToInt(InitialHealth + BaseHealth * Mathf.Pow(duelistLevel, HealthExponent));

        newHealth += Random.Range(-newHealth / 10, newHealth / 10);

        newHealth = (int)(newHealth / 5f);
        newHealth *= 5;

        return newHealth;
    }

    public static int GenerateDuelistPower(int duelistLevel)
    {
        return Mathf.RoundToInt(InitialPower + BasePower * Mathf.Pow(duelistLevel, PowerExponent));
    }

    public static int GenerateUpgradePowerRate(int power)
    {
        return InitialUpgradePowerRate + Mathf.RoundToInt(power / 10f);
    }
}
