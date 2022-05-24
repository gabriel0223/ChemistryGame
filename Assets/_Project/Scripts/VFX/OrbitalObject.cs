using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OrbitalObject : MonoBehaviour
{
    [SerializeField] private RectTransform _positionA;
    [SerializeField] private RectTransform _positionB;
    [SerializeField] private float _objectVelocity;
    [SerializeField] private float _delayBetweenLoops;
    [SerializeField] private float _initialDelay;

    private RectTransform _rectTransform;
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        _rectTransform.anchoredPosition = _positionA.anchoredPosition;
        
        Sequence orbitSequence = DOTween.Sequence().SetLoops(-1, LoopType.Restart);

        orbitSequence.PrependInterval(_initialDelay);
        orbitSequence.Append(_rectTransform.DOAnchorPos(_positionB.anchoredPosition, 60 / _objectVelocity).SetEase(Ease.Linear));
        orbitSequence.AppendInterval(_delayBetweenLoops);
    }
}
