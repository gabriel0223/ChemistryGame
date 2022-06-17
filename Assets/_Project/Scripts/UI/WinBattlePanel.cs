using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WinBattlePanel : EndBattlePanel
{
    [SerializeField] private ExperienceLevelBar _levelBar;
    [SerializeField] private TMP_Text _moneyGainedText;

    private GamePersistentData _gamePersistentData;
    private int _moneyGained;
    private int _experienceGained;
    private const float MoneyCountingDuration = 3f;

    public override void Initialize()
    {
        base.Initialize();

        _gamePersistentData = GamePersistentData.Instance;
        int duelistBeatenLevel = _gamePersistentData.CurrentLevelData.DuelistData.Level;
        
        _moneyGained = RewardSettings.GetMoneyRewardForWin(duelistBeatenLevel);
        _experienceGained = RewardSettings.GetExperienceRewardForWin(duelistBeatenLevel);
        Debug.Log($"XP GAINED: {_experienceGained}");
        Debug.Log($"MAX XP IN LEVEL: {ExperienceLevelData.LevelData[1]}");

        _levelBar.Initialize();
        _moneyGainedText.SetText("0");

        _gamePersistentData.AddMoney(_moneyGained);
        _gamePersistentData.AddExperiencePoints(_experienceGained);
        _gamePersistentData.CurrentLevelData.LevelState = LevelState.Done;
        _gamePersistentData.SavePlayerData();
    }

    public override void HandlePosYTweenEnd()
    {
        Sequence rewardSequence = DOTween.Sequence();
        
        rewardSequence.AppendCallback(() => _levelBar.AnimateBar(_experienceGained));
        rewardSequence.AppendInterval(ExperienceLevelBar.BarAnimationDuration);
        rewardSequence.Append(DOVirtual.Int(0, _moneyGained, MoneyCountingDuration, 
            value => _moneyGainedText.SetText(value.ToString())));
    }
}
