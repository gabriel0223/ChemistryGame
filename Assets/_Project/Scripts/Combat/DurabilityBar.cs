using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{
    [SerializeField] private float _animationDuration;

    private Image _durabilityBar;

    private void Awake()
    {
        _durabilityBar = GetComponent<Image>();
    }

    public void AnimateBar(int newDurability)
    {
        _durabilityBar.DOFillAmount( (float)newDurability / 3, _animationDuration);
    }

    public void ChangeColor(Color newColor)
    {
        _durabilityBar.DOColor(newColor, 0.5f);
    }
}
