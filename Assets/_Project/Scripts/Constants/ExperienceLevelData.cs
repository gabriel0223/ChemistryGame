using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExperienceLevelData
{
    //FORMULA: baseXP * (level ^ exponent)
    
    private const float BaseXp = 100f;
    private const float Exponent = 1.5f;
    private const int NumberOfLevels = 50;

    public static readonly Dictionary<int, int> LevelData = new Dictionary<int, int>();

    private static void InitializeDictionary()
    {
        for (int i = 1; i <= NumberOfLevels; i++)
        {
            LevelData.Add(i, Mathf.RoundToInt(BaseXp * Mathf.Pow(i, Exponent)));
        }
    }

    public static float GetMaxXpInLevel(int level)
    {
        if (LevelData.Count == 0)
        {
            InitializeDictionary();
        }

        return LevelData[level];
    }
    
    // public static Dictionary<int, int> LevelData = new Dictionary<int, int>()
    // {
    //     { 1, 100 },
    //     { 2, 200 }
    // };
}
