using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundPlanetView : MonoBehaviour
{
    private Image _image;
    private GamePersistentData _gamePersistentData;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _gamePersistentData = GamePersistentData.Instance;
        
        Material planetMaterial = Instantiate(_image.material);
        _image.material = planetMaterial;
        planetMaterial.SetFloat("_HsvShift", _gamePersistentData.CurrentLevelData.PlanetColor);
    }
}
