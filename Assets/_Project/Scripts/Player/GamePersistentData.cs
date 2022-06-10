using System;
using System.Collections;
using System.Collections.Generic;
using Triplano.SaveSystem;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GamePersistentData : MonoBehaviour
{
    public static GamePersistentData Instance;
    
    [SerializeField] private int _level;
    [SerializeField] private int _experiencePoints;
    [SerializeField] private int _money;

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

    private void SavePlayerData()
    {
        SaveSystem.Data.Level = _level;
        SaveSystem.Data.ExperiencePoints = _experiencePoints;
        SaveSystem.Data.Money = _money;
        SaveSystem.SaveGame();
        SaveSystem.LoadGame();
    }

    private void LoadPlayerData()
    {
        SaveSystem.LoadGame();
    }
}
