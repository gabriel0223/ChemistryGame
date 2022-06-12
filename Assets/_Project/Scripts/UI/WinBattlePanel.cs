using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinBattlePanel : EndBattlePanel
{
    [SerializeField] private ExperienceLevelBar _levelBar;
    [SerializeField] private TMP_Text _moneyGainedText;

    private GamePersistentData _gamePersistentData;
    private int _moneyGained;
    private int _experienceGained;

    public override void Initialize()
    {
        base.Initialize();

        _gamePersistentData = GamePersistentData.Instance;
        int duelistBeatenLevel = _gamePersistentData.CurrentLevelData.DuelistData.Level;
        
        _moneyGained = RewardSettings.GetMoneyRewardForWin(duelistBeatenLevel);
        _experienceGained = RewardSettings.GetExperienceRewardForWin(duelistBeatenLevel);

        _gamePersistentData.AddMoney(_moneyGained);
        _gamePersistentData.AddExperiencePoints(_experienceGained);
        //_gamePersistentData.SavePlayerData();
        
        _levelBar.AnimateBar(_experienceGained);
    }

    public void StartUIAnimation()
    {
        _moneyGainedText.SetText(_moneyGained.ToString());
    }
}
