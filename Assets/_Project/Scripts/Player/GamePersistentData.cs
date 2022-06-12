using System;
using System.Collections;
using System.Collections.Generic;
using Triplano.SaveSystem;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GamePersistentData : MonoBehaviour
{
    public static GamePersistentData Instance;
    
    public int PlayerLevel;
    public int PlayerExperiencePoints;
    public int PlayerMoney;
    public int PlayerHealth;
    
    public PlanetData CurrentLevelData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        SaveSystem.Data.Level = PlayerLevel;
        SaveSystem.Data.ExperiencePoints = PlayerExperiencePoints;
        SaveSystem.Data.Health = PlayerHealth;
        SaveSystem.Data.Money = PlayerMoney;
        
        SaveSystem.SaveGame();
        SaveSystem.LoadGame();
    }

    public void AddExperiencePoints(int xp)
    {
        PlayerExperiencePoints += xp;
    }

    public void AddMoney(int money)
    {
        PlayerMoney += money;
    }

    private void LoadPlayerData()
    {
        SaveSystem.LoadGame();

        SaveSystem.SaveData playerData = SaveSystem.Data;

        PlayerLevel = playerData.Level;
        PlayerExperiencePoints = playerData.ExperiencePoints;
        PlayerMoney = playerData.Money;
        PlayerHealth = PlayerProgressionSettings.GeneratePlayerHealthByLevel(PlayerLevel);
    }

    [ContextMenu("Delete Save Files")]
    public void DeleteSaveFiles()
    {
        SaveSystem.ClearSavedData();
    }
}
