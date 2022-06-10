using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Triplano.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceLevelBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private TMP_Text _levelNumberText;
    
    private int _level;
    private int _experiencePoints;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        SaveSystem.SaveData playerData = SaveSystem.Data;

        _level = playerData.Level;
        _experiencePoints = playerData.ExperiencePoints;
        
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        _levelNumberText.SetText(_level.ToString());
        _bar.fillAmount = _experiencePoints / ExperienceLevelData.GetMaxXpInLevel(_level);
    }
}
