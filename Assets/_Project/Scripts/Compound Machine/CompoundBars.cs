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
        
        AnimateBar(_atomicNumberBar, element.AtomicNumber.PropertyQuantity);
        AnimateBar(_electronegativityBar, element.Electronegativity.PropertyQuantity);
        AnimateBar(_atomicRadiusBar, element.AtomicRadius.PropertyQuantity);
    }

    private void AnimateBar(Image bar, PropertyQuantity propertyQuantity)
    {
        float fillMultiplier = propertyQuantity switch
        {
            PropertyQuantity.Minimum => -2,
            PropertyQuantity.Low => -1,
            PropertyQuantity.High => 1,
            PropertyQuantity.Maximum => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(propertyQuantity), propertyQuantity, null)
        };
        
        float barFillAmount = bar.fillAmount + _barMinimumStep * fillMultiplier;
        
        barFillAmount = Mathf.Clamp(barFillAmount, _barMinimumStep, 1);
        
        bar.DOFillAmount(barFillAmount, _barAnimationDuration)
            .SetEase(_barAnimationCurve);
    }

    private void ResetBars()
    {
        _atomicNumberBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
        _electronegativityBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
        _atomicRadiusBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
    }
}
