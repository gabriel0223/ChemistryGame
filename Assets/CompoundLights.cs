using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompoundLights : MonoBehaviour
{
    [SerializeField] private CompoundSlot _compoundSlot;
    [SerializeField] private Image[] _lights;
    [SerializeField] private Color _fullColor;
    [SerializeField] private Color _emptyColor;

    private int _lightIndex;

    private void OnEnable()
    {
        _compoundSlot.OnAddToCompound += TurnLightOn;
        _compoundSlot.OnResetCompound += ResetLights;
    }

    private void OnDisable()
    {
        _compoundSlot.OnResetCompound -= ResetLights;
    }

    private void TurnLightOn(Element element)
    {
        _lights[_lightIndex].color = _fullColor;
        _lightIndex++;
    }

    private void ResetLights()
    {
        foreach (var light in _lights)
        {
            light.color = _emptyColor;
        }

        _lightIndex = 0;
    }
}
