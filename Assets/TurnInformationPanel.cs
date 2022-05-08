using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurnInformationPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _turnInformationText;
    [SerializeField] private RectTransform _textEndPos;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _pauseDuration;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        PlayInformationPanelAnimation();
    }

    private void PlayInformationPanelAnimation()
    {
        Sequence tweenSequence = DOTween.Sequence();

        tweenSequence.Append(_image.DOFade(0.5f, _fadeDuration));
        tweenSequence.Join(_turnInformationText.DOAnchorPosY(0, _fadeDuration));
        
        tweenSequence.AppendInterval(_pauseDuration);
        
        tweenSequence.Append(_image.DOFade(0f, _fadeDuration));
        tweenSequence.Join(_turnInformationText.DOAnchorPosY(_textEndPos.anchoredPosition.y, _fadeDuration));
        
        tweenSequence.OnComplete(() => Destroy(gameObject));
    }

    public float GetAnimationDuration()
    {
        return (_fadeDuration * 2) + _pauseDuration;
    }
}
