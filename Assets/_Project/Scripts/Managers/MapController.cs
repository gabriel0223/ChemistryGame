using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Triplano.SaveSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject[] _planetPrefabs;
    [SerializeField] private Transform _planetContainer;
    [SerializeField] private DuelConfirmationBox _duelConfirmationBox;
    [SerializeField] private DuelistVisualData _duelistVisualData;

    private const int NumberOfPlanetsOnMap = 2;

    private void Start()
    {
       InitializeMap();
    }

    private void InitializeMap()
    {
        bool areTherePlanetsSaved = SaveSystem.Data.CurrentPlanets.Count > 0;
        
        foreach (Transform planet in _planetContainer)
        {
            Destroy(planet.gameObject);
        }

        if (areTherePlanetsSaved)
        {
            Debug.Log("GET SAVED PLANETS");
            GetSavedPlanets();
        }
        else
        {
            Debug.Log("GENERATE PLANETS");
            
            for (int i = 0; i < NumberOfPlanetsOnMap; i++)
            {
                GeneratePlanet();
            }
        }
    }

    private void GetSavedPlanets()
    {
        SaveSystem.SaveData playerData = SaveSystem.Data;

        foreach (var planetData in playerData.CurrentPlanets)
        {
            PlanetLevelController planet = Instantiate(_planetPrefabs[Random.Range(0, _planetPrefabs.Length)], _planetContainer)
                .GetComponent<PlanetLevelController>();
        
            planet.SetPlanetData(planetData);

            if (planetData.LevelState == LevelState.Available)
            {
                planet.PlanetButton.onClick.AddListener(() => _duelConfirmationBox.OpenBox(planetData));
            }
        }
    }

    private void GeneratePlanet()
    {
        SaveSystem.SaveData playerData = SaveSystem.Data;

        int lastPlanetNumber = playerData.CurrentPlanets.Count == 0 ? 0 : playerData.CurrentPlanets.LastOrDefault().PlanetNumber;
        LevelState levelState = playerData.CurrentPlanets.Count == 0 ? LevelState.Available : LevelState.Locked;

        PlanetData planetData = new PlanetData
        {
            DuelistData = GenerateDuelist(),
            LevelState = levelState,
            PlanetColor = Random.Range(0, 360),
            PlanetNumber = lastPlanetNumber + 1,
        };

        PlanetLevelController planet = Instantiate(_planetPrefabs[Random.Range(0, _planetPrefabs.Length)], _planetContainer)
            .GetComponent<PlanetLevelController>();
        
        planet.SetPlanetData(planetData);
        SaveSystem.Data.CurrentPlanets.Add(planetData);
        SaveSystem.SaveGame();
    }

    private DuelistData GenerateDuelist()
    {
        DuelistData duelistData = new DuelistData();
        SaveSystem.SaveData playerData = SaveSystem.Data;

        duelistData.Level = playerData.Level;
        duelistData.Name = DuelistNameGenerator.GenerateDuelistName();

        duelistData.FaceIndex = Random.Range(0, _duelistVisualData.DuelistFacesPrefabs.Length);
        duelistData.EyesIndex = Random.Range(0, _duelistVisualData.DuelistEyes.Length);
        duelistData.MouthIndex = Random.Range(0, _duelistVisualData.DuelistMouths.Length);
        duelistData.NoseIndex = Random.Range(0, _duelistVisualData.Noses.Length);
        duelistData.BodyIndex = Random.Range(0, _duelistVisualData.Bodies.Length);
        duelistData.AccessoryIndex = Random.Range(0, _duelistVisualData.Accessories.Length + (_duelistVisualData.Accessories.Length / 2));
        duelistData.HueColor = Random.Range(0, 360);

        return duelistData;
    }
}
