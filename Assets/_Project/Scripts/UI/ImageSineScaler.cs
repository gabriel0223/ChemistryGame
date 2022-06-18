using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ImageSineScaler : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _scalingSpeed;

    [SerializeField] private float _rotationScale;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, -_rotationScale);
        
        rectTransform.DOScale(Vector3.one * _scaleMultiplier, 1 / _scalingSpeed)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        
        rectTransform.DORotate(Vector3.forward * _rotationScale, 1 / _rotationSpeed)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
