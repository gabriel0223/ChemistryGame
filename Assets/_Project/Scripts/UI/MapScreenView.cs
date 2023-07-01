using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreenView : MonoBehaviour
{
    [SerializeField] private Image _mapButton;
    [SerializeField] private Image _periodicTableButton;
    
    [SerializeField] private Sprite _mapSelectedSprite;
    [SerializeField] private Sprite _mapUnselectedSprite;
    [SerializeField] private Sprite _periodicTableSelectedSprite;
    [SerializeField] private Sprite _periodicTableUnselectedSprite;
    
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _periodicTable;

    private void OnEnable()
    {
        EnableMap();
    }

    public void EnableMap()
    {
        _map.SetActive(true);
        _periodicTable.SetActive(false);
        _mapButton.sprite = _mapSelectedSprite;
        _periodicTableButton.sprite = _periodicTableUnselectedSprite;
    }
    
    public void EnablePeriodicTable()
    {
        _periodicTable.SetActive(true);
        _map.SetActive(false);
        _periodicTableButton.sprite = _periodicTableSelectedSprite;
        _mapButton.sprite = _mapUnselectedSprite;
    }
}
