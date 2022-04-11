using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompoundBars : MonoBehaviour
{
    [SerializeField] private CompoundSlot _compoundSlot;
    [SerializeField] private Image electronegativityBar;
    [SerializeField] private Image atomicNumberBar;
    [SerializeField] private Image atomicRadiusBar;

    private void OnEnable()
    {
        _compoundSlot.OnAddToCompound += UpdateBars;
    }

    private void OnDisable()
    {
        _compoundSlot.OnAddToCompound -= UpdateBars;
    }

    public void UpdateBars()
    {
        
    }
}
