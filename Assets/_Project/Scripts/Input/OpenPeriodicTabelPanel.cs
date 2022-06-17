using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class OpenPeriodicTabelPanel : MonoBehaviour
{
    [SerializeField] private float timeToOpenPanel;
    [SerializeField] private float timeToClosePanel;
    private OpenSoundSettingsPanel _soundSettingsPanel;

    private bool _isOpen;

    private void Awake()
    {
        _soundSettingsPanel = FindObjectOfType<OpenSoundSettingsPanel>();
    }

    private void Start()
    {
        transform.localScale = Vector2.zero;
        _isOpen = false;
    }

    public void OpenPeriodicTablePanel()
    {
        if (!_isOpen)
        {
            _soundSettingsPanel.CloseSettingsPanel();
            transform.LeanScale(new Vector3(1,1,1), timeToOpenPanel);
            _isOpen = true;
        }
        else 
        {
            transform.LeanScale(new Vector3(0,0,0), timeToClosePanel);
            _isOpen = false;
        }
    }

    public void ClosePeriodicTablePanel()
    {
        _isOpen = true;
        OpenPeriodicTablePanel();
    }
}
