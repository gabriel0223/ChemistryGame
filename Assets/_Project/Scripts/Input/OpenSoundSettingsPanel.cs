using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSoundSettingsPanel : MonoBehaviour
{
    [SerializeField] private float timeToOpenPanel;
    [SerializeField] private float timeToClosePanel;
    private OpenPeriodicTabelPanel _periodicTablePanel;

    private void Awake()
    {
        _periodicTablePanel = FindObjectOfType<OpenPeriodicTabelPanel>();
    }

    private void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void OpenSettingsPanel()
    {
        _periodicTablePanel.ClosePeriodicTablePanel();
        transform.LeanScale(new Vector3(1,1,1), timeToOpenPanel);
    }
    
    public void CloseSettingsPanel()
    {
        transform.LeanScale(new Vector3(0,0,0), timeToClosePanel);
    }
}
