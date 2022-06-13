using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetLevelController : MonoBehaviour
{
    [SerializeField] private Image _planetImage;
    [SerializeField] private Button _planetButton;
    [SerializeField] private TMP_Text _planetNumberText;
    [SerializeField] private GameObject _lockedIcon;
    [SerializeField] private GameObject _shipOrbit;
    [SerializeField] private GameObject _doneIcon;
    
    private PlanetData _planetData;

    public Button PlanetButton => _planetButton;

    public void SetPlanetData(PlanetData planetData)
    {
        _planetData = planetData;

        UpdatePlanetUI();
    }

    private void UpdatePlanetUI()
    {
        Material planetMaterial = Instantiate(_planetImage.material);

        _planetImage.material = planetMaterial;
        planetMaterial.SetFloat("_HsvShift", _planetData.PlanetColor);
        planetMaterial.SetFloat("_HsvSaturation", _planetData.PlanetSaturation);
        _planetNumberText.SetText("Duelo " + _planetData.PlanetNumber);

        switch (_planetData.LevelState)
        {
            case LevelState.Locked:
                SetIconActive(_lockedIcon);
                break;
            
            case LevelState.Available:
                SetIconActive(_shipOrbit);
                _planetNumberText.transform.localPosition = Vector3.zero;
                break;
            
            case LevelState.Done:
                SetIconActive(_doneIcon);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetIconActive(GameObject icon)
    {
        _lockedIcon.SetActive(false);
        _shipOrbit.SetActive(false);
        _doneIcon.SetActive(false);
        
        icon.SetActive(true);
    }
}
