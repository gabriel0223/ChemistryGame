using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ImageSineMover : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    
    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + _amplitude, 1 / _frequency)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
