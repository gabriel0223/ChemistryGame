using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Triplano.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceLevelBar : MonoBehaviour
{
    public const float BarAnimationDuration = 3f;

    [SerializeField] private Image _bar;
    [SerializeField] private TMP_Text _levelNumberText;

    private GamePersistentData _gamePersistentData;

    private bool _hasInitialized;
    private int _level;
    private int _experiencePoints;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (_hasInitialized)
        {
            return;
        }
        
        _gamePersistentData = GamePersistentData.Instance;

        _level = _gamePersistentData.PlayerLevel;
        _experiencePoints = _gamePersistentData.PlayerExperiencePoints;
        
        UpdateLevelUI();

        _hasInitialized = true;
    }

    private void UpdateLevelUI()
    {
        _levelNumberText.SetText(_level.ToString());
        _bar.fillAmount = (_experiencePoints - ExperienceLevelData.GetMaxXpInLevel(_level - 1)) / ExperienceLevelData.GetMaxXpInLevel(_level);
    }

    public void AnimateBar(float newExperiencePoints)
    {
        if (!_hasInitialized)
        {
            Initialize();
        }
        
        Sequence levelBarSequence = DOTween.Sequence();
        float newFillAmount;

        if (_experiencePoints + newExperiencePoints > ExperienceLevelData.LevelData[_level])
        {
            newFillAmount = ((_experiencePoints + newExperiencePoints) - ExperienceLevelData.GetMaxXpInLevel(_level)) / ExperienceLevelData.GetMaxXpInLevel(_level + 1);

            levelBarSequence.Append(_bar.DOFillAmount(1, BarAnimationDuration / 2));
            levelBarSequence.AppendCallback(LevelUp);
            levelBarSequence.Append(_bar.DOFillAmount(newFillAmount, BarAnimationDuration / 2));
        }
        else
        {
            newFillAmount = ((_experiencePoints + newExperiencePoints) - ExperienceLevelData.GetMaxXpInLevel(_level - 1)) / ExperienceLevelData.GetMaxXpInLevel(_level);

            levelBarSequence.Append(_bar.DOFillAmount(newFillAmount, BarAnimationDuration));
        }

        void LevelUp()
        {
            _levelNumberText.SetText((_level + 1).ToString());
            _bar.fillAmount = 0;
        }
    }
}
