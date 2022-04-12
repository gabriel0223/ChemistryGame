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
    }

    private void OnDisable()
    {
        _compoundSlot.OnAddToCompound -= UpdateBars;
    }

    private void UpdateBars(Element element)
    {
        int atomicNumberMultiplier = element.AtomicNumber.PropertyQuantity == PropertyQuantity.Low ? -1 : 1;
        int electronegativityMultiplier = element.Electronegativity.PropertyQuantity == PropertyQuantity.Low ? -1 : 1;
        int atomicRadiusMultiplier = element.AtomicRadius.PropertyQuantity == PropertyQuantity.Low ? -1 : 1;
        
        AnimateBar(_atomicNumberBar, atomicNumberMultiplier);
        AnimateBar(_electronegativityBar, electronegativityMultiplier);
        AnimateBar(_atomicRadiusBar, atomicRadiusMultiplier);
    }

    private void AnimateBar(Image bar, int quantityMultiplier)
    {
        bar.DOFillAmount(_atomicNumberBar.fillAmount - _barMinimumStep * quantityMultiplier, _barAnimationDuration)
            .SetEase(_barAnimationCurve);
    }
}
