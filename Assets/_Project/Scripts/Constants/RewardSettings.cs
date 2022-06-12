using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RewardSettings
{
    private const float BaseMoney = 100f;
    private const float MoneyExponent = 0.5f;
    
    private const float BaseXp = 75f;
    private const float ExperienceExponent = 1.25f;

    public static int GetMoneyRewardForWin(int duelistLevel)
    {
        return Mathf.RoundToInt(BaseMoney * (Mathf.Pow(duelistLevel, MoneyExponent)));
    }
    
    public static int GetExperienceRewardForWin(int duelistLevel)
    {
        return Mathf.RoundToInt(BaseXp * (Mathf.Pow(duelistLevel, ExperienceExponent)));
    }
}
