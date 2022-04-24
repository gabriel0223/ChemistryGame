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
        AnimateBar(_atomicNumberBar, (int)element.AtomicNumber.PropertyQuantity);
        AnimateBar(_electronegativityBar, (int)element.Electronegativity.PropertyQuantity);
        AnimateBar(_atomicRadiusBar, (int)element.AtomicRadius.PropertyQuantity);
    }

    private void AnimateBar(Image bar, int quantityMultiplier)
    {
        bar.DOFillAmount(_atomicNumberBar.fillAmount - _barMinimumStep * quantityMultiplier, _barAnimationDuration)
            .SetEase(_barAnimationCurve);
    }

    private void ResetBars()
    {
        _atomicNumberBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
        _electronegativityBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
        _atomicRadiusBar.DOFillAmount(0.5f, _barAnimationDuration).SetEase(_barAnimationCurve);
    }
}
