using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RewardSettings
{
    private const float BaseMoney = 100f;
    private const float MoneyExponent = 0.5f;
    
    private const float BaseXp = 75f;
    private const float ExperienceExponent = 1.1f;

    public static int GetMoneyRewardForWin(int duelistLevel)
    {
        int moneyWon = Mathf.RoundToInt(BaseMoney * (Mathf.Pow(duelistLevel, MoneyExponent)));

        return moneyWon + Random.Range(-moneyWon / 10, moneyWon / 10);
    }
    
    public static int GetExperienceRewardForWin(int duelistLevel)
    {
        int xpWon = Mathf.RoundToInt(BaseXp * (Mathf.Pow(duelistLevel, ExperienceExponent)));

        return xpWon + Random.Range(-xpWon / 10, xpWon / 10);
    }
}
