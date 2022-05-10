using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{
    [SerializeField] private Color _goodDurabilityColor;
    [SerializeField] private Color _mediumDurabilityColor;
    [SerializeField] private Color _badDurabilityColor;
    [SerializeField] private float _animationDuration;

    private Image _durabilityBar;

    private void Awake()
    {
        _durabilityBar = GetComponent<Image>();
    }

    public void AnimateBar(int newDurability)
    {
        Color newBarColor = newDurability switch
        {
            0 => _badDurabilityColor,
            1 => _badDurabilityColor,
            2 => _mediumDurabilityColor,
            3 => _goodDurabilityColor,
            _ => throw new ArgumentOutOfRangeException(nameof(newDurability), newDurability, null)
        };

        _durabilityBar.DOColor(newBarColor, _animationDuration);
        _durabilityBar.DOFillAmount( (float)newDurability / 3, _animationDuration);
    }
    
    //public void 
}
