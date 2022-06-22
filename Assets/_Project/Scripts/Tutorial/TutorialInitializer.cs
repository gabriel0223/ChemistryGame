using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialInitializer : MonoBehaviour
{
    [FormerlySerializedAs("_customDuelist")] [SerializeField] private CustomPlanetDataSO customPlanet;
    [SerializeField] private Button _playButton;

    private GamePersistentData _gamePersistentData;

    private void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;

        if (!_gamePersistentData.IsPlayingFirstTime)
        {
            return;
        }
        
        _gamePersistentData.CurrentLevelData = customPlanet.PlanetData;
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(() => SceneManager.LoadScene((int)SceneIndexes.GAME));
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }
}
