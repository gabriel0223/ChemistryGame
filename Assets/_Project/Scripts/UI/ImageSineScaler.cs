using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ImageSineScaler : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _scalingSpeed;

    private void Start()
    {
        GetComponent<RectTransform>().DOScale(Vector3.one * _scaleMultiplier, 1 / _scalingSpeed)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
