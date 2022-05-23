using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpeechBubbleController : MonoBehaviour
{
    [SerializeField] private float _spawnAnimationDuration;
    [SerializeField] private float _intervalBeforeDestruction;
    [SerializeField] private float _destructionAnimationDuration;
    
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.one, _spawnAnimationDuration).SetEase(Ease.OutBack));
        sequence.AppendInterval(_intervalBeforeDestruction);
        sequence.Append(transform.DOScale(Vector3.zero, _destructionAnimationDuration));
        sequence.AppendCallback(() => Destroy(gameObject));
    }
}
