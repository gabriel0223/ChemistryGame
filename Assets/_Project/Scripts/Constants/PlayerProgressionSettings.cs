using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerProgressionSettings
{
    public const int CardsWonEachLevelUp = 5;
    
    private const float InitialHealth = 90f;
    private const float BaseHealth = 10f;
    private const float HealthExponent = 1.5f;

    public static int GeneratePlayerHealthByLevel(int level)
    {
        int newHealth = Mathf.RoundToInt(InitialHealth + BaseHealth * Mathf.Pow(level, HealthExponent));

        newHealth = (int)(newHealth / 5f);
        newHealth *= 5;

        return newHealth;
    }
}
