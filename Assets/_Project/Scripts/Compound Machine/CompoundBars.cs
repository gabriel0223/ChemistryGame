using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CompoundBars : MonoBehaviour
{
    [SerializeField] private CompoundSlot _compoundSlot;
    [SerializeField] private Image _atomicNumberBar;
    [SerializeField] private Image _electronegativityBar;
    [SerializeField] private Image _atomicRadiusBar;
    [SerializeField] private float _barMinimumStep;

    [Header("Animation Parameters")] 
    [SerializeField] private float _barAnimationDuration;
    [SerializeField] private AnimationCurve _barAnimationCurve;

    private int _atomicNumberLevel = 2;
    private int _electronegativityLevel = 2;
    private int _atomicRadiusLevel = 2;

    private void OnEnable()
    {
        _compoundSlot.OnAddToCompound += UpdateBars;
        _compoundSlot.OnResetCompound += ResetBars;
    }

    private void OnDisable()
    {
        _compoundSlot.OnAddToCompound -= UpdateBars;
        _compoundSlot.OnResetCompound -= ResetBars;
    }

    private void UpdateBars(Element element)
    {
        if (element.ElementData.Atomicnumber == 1)
        {
            return;
        }
        
        AnimateBar(_atomicNumberBar, element.AtomicNumber.PropertyQuantity, ref _atomicNumberLevel);
        AnimateBar(_electronegativityBar, element.Electronegativity.PropertyQuantity, ref _electronegativityLevel);
        AnimateBar(_atomicRadiusBar, element.AtomicRadius.PropertyQuantity, ref _atomicRadiusLevel);
    }

    private void AnimateBar(Image bar, PropertyQuantity propertyQuantity, ref int barLevel)
    {
        int fillMultiplier = propertyQuantity switch
        {
            PropertyQuantity.Minimum => -2,
            PropertyQuantity.Low => -1,
            PropertyQuantity.High => 1,
            PropertyQuantity.Maximum => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(propertyQuantity), propertyQuantity, null)
        };

        barLevel += fillMultiplier;
        barLevel = Mathf.Clamp(barLevel, 1, 4);

        float barFillAmount = (float)barLevel / 4;
        barFillAmount = Mathf.Clamp(barFillAmount, _barMinimumStep, 1);
        
        bar.DOFillAmount(barFillAmount, _barAnimationDuration)
            .SetEase(_barAnimationCurve);
    }

    private void ResetBars()
    {
        _atomicNumberBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
        _electronegativityBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
        _atomicRadiusBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);

        _atomicNumberLevel = 2;
        _electronegativityLevel = 2;
        _atomicRadiusLevel = 2;
    }
}
