using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShipOrbit : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    
    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DORotate(new Vector3(rectTransform.rotation.eulerAngles.x,
            rectTransform.rotation.eulerAngles.y, 180), 1 / _rotationSpeed)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
