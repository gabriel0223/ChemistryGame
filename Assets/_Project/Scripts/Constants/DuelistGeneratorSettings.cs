using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DuelistGeneratorSettings
{
    private const float InitialHealth = 40f;
    private const float BaseHealth = 10f;
    private const float HealthExponent = 1.75f;

    public static int GenerateDuelistHealth(int duelistLevel)
    {
        int newHealth = Mathf.RoundToInt(InitialHealth + BaseHealth * Mathf.Pow(duelistLevel, HealthExponent));

        newHealth += Random.Range(-newHealth / 10, newHealth / 10);

        newHealth = (int)(newHealth / 5f);
        newHealth *= 5;

        return newHealth;
    }
}
