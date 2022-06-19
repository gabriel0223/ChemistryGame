using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInitializer : MonoBehaviour
{
    [SerializeField] private CustomDuelistDataSO _customDuelist;

    private GamePersistentData _gamePersistentData;

    private void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;

        if (!_gamePersistentData.IsPlayingFirstTime)
        {
            return;
        }
        
        _gamePersistentData.CurrentLevelData.DuelistData = _customDuelist.DuelistData;
    }
}
