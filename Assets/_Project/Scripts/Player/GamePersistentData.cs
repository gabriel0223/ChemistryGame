using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Triplano.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GamePersistentData : MonoBehaviour
{
    public static GamePersistentData Instance;
    
    public int PlayerLevel;
    public int PlayerExperiencePoints;
    public int PlayerMoney;
    public int PlayerHealth;
    public List<int> CardsInPossession;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadPlayerData();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        SaveSystem.Data.Level = PlayerLevel;
        SaveSystem.Data.ExperiencePoints = PlayerExperiencePoints;
        SaveSystem.Data.Health = PlayerHealth;
        SaveSystem.Data.Money = PlayerMoney;
        SaveSystem.Data.CurrentPlanets[0] = CurrentLevelData;
        SaveSystem.Data.CardsInPossession = CardsInPossession;
        
        SaveSystem.SaveGame();
        SaveSystem.LoadGame();
    }

    public void AddExperiencePoints(int xp)
    {
        PlayerExperiencePoints += xp;

        if (PlayerExperiencePoints > ExperienceLevelData.LevelData[PlayerLevel])
        {
            HandleLevelUp();
        }
    }

    private void HandleLevelUp()
    {
        PlayerLevel++;

        for (int i = 0; i < PlayerProgressionSettings.CardsWonEachLevelUp; i++)
        {
            CardsInPossession.Add(CardsInPossession.LastOrDefault() + 1);
        }
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
        CardsInPossession = playerData.CardsInPossession;
    }

    [ContextMenu("Delete Save Files")]
    public void DeleteSaveFiles()
    {
        SaveSystem.ClearSavedData();
    }
}
