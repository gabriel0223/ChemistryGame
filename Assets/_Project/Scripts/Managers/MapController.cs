using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Triplano.SaveSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    [SerializeField] private RectTransform _mapTilesTransform;
    [SerializeField] private RectTransform _mapScrollDownPosition;
    [SerializeField] private GameObject[] _planetPrefabs;
    [SerializeField] private Transform _planetContainer;
    [SerializeField] private DuelConfirmationBox _duelConfirmationBox;
    [SerializeField] private DuelistVisualData _duelistVisualData;

    private List<PlanetLevelController> _planetsOnMap = new List<PlanetLevelController>();

    private const int NumberOfPlanetsOnMap = 2;
    private const float ScrollMapDelay = 1f;

    private void Start()
    {
       InitializeMap();
       AudioManager.instance.Play("briefing");
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
                GenerateNewPlanet();
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
            
            _planetsOnMap.Add(planet);
            planet.SetPlanetData(planetData);

            switch (planetData.LevelState)
            {
                case LevelState.Available:
                    planet.PlanetButton.onClick.AddListener(() => _duelConfirmationBox.OpenBox(planetData));
                    break;
            }
        }

        PlanetData firstPlanet = playerData.CurrentPlanets[0];
        
        if (firstPlanet.LevelState == LevelState.Done)
        {
            playerData.CurrentPlanets.Remove(firstPlanet);
            _planetsOnMap.RemoveAt(0);
            GenerateNewPlanet();
            UnlockNextPlanet();
            ScrollMapDown();
        }
    }

    private void GenerateNewPlanet()
    {
        SaveSystem.SaveData playerData = SaveSystem.Data;

        int lastPlanetNumber = playerData.CurrentPlanets.Count == 0 ? 0 : playerData.CurrentPlanets.LastOrDefault().PlanetNumber;
        LevelState levelState = playerData.CurrentPlanets.Count == 0 ? LevelState.Available : LevelState.Locked;

        PlanetData planetData = new PlanetData
        {
            DuelistData = GenerateDuelist(),
            LevelState = levelState,
            PlanetColor = Random.Range(0, 360),
            PlanetSaturation = Random.Range(0.75f, 1f),
            PlanetNumber = lastPlanetNumber + 1,
        };

        PlanetLevelController planet = Instantiate(_planetPrefabs[Random.Range(0, _planetPrefabs.Length)], _planetContainer)
            .GetComponent<PlanetLevelController>();
        
        _planetsOnMap.Add(planet);
        planet.SetPlanetData(planetData);
        
        if (planetData.LevelState == LevelState.Available)
        {
            planet.PlanetButton.onClick.AddListener(() => _duelConfirmationBox.OpenBox(planetData));
        }
        
        SaveSystem.Data.CurrentPlanets.Add(planetData);
        SaveSystem.SaveGame();
    }

    private DuelistData GenerateDuelist()
    {
        DuelistData duelistData = new DuelistData();
        SaveSystem.SaveData playerData = SaveSystem.Data;

        duelistData.Level = playerData.Level;
        duelistData.Name = DuelistNameGenerator.GenerateDuelistName();
        duelistData.Health = DuelistGeneratorSettings.GenerateDuelistHealth(duelistData.Level);
        duelistData.Power = DuelistGeneratorSettings.GenerateDuelistPower(duelistData.Level);
        duelistData.UpgradePowerRate = DuelistGeneratorSettings.GenerateUpgradePowerRate(duelistData.Power);

        duelistData.FaceIndex = Random.Range(0, _duelistVisualData.DuelistFacesPrefabs.Length);
        duelistData.EyesIndex = Random.Range(0, _duelistVisualData.DuelistEyes.Length);
        duelistData.MouthIndex = Random.Range(0, _duelistVisualData.DuelistMouths.Length);
        duelistData.NoseIndex = Random.Range(0, _duelistVisualData.Noses.Length);
        duelistData.BodyIndex = Random.Range(0, _duelistVisualData.Bodies.Length);
        duelistData.AccessoryIndex = Random.Range(0, _duelistVisualData.Accessories.Length + (_duelistVisualData.Accessories.Length * 2));
        duelistData.HueColor = Random.Range(0, 360);

        return duelistData;
    }

    private void UnlockNextPlanet()
    {
        PlanetData nextPlanetData = SaveSystem.Data.CurrentPlanets[0];
        nextPlanetData.LevelState = LevelState.Available;
        SaveSystem.Data.CurrentPlanets[0] = nextPlanetData;
        _planetsOnMap[0].SetPlanetData(nextPlanetData);
        _planetsOnMap[0].PlanetButton.onClick.AddListener(() => _duelConfirmationBox.OpenBox(nextPlanetData));
        
        SaveSystem.SaveGame();
    }

    private void ScrollMapDown()
    {
        Sequence scrollMapSequence = DOTween.Sequence();

        scrollMapSequence.AppendInterval(ScrollMapDelay);
        scrollMapSequence.Append(_mapTilesTransform.DOAnchorPosY(_mapScrollDownPosition.anchoredPosition.y, 2.5f));
    }
}
