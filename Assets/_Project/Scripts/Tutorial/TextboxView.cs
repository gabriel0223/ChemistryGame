using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class TextboxView : MonoBehaviour
{
    [SerializeField] private TextAnimatorPlayer _dialogueText;

    private RectTransform _rectTransform;
    private bool _hasInitialized;
    private Sequence textboxAnimSequence;
    private const float InitializationTime = 0.5f;

    public TextAnimatorPlayer DialogueText => _dialogueText;
    public bool HasInitialized => _hasInitialized;

    private void Start()
    {
        _rectTransform.localScale = Vector3.zero;
        _dialogueText.ShowText("");
        DOVirtual.DelayedCall( InitializationTime,() => _hasInitialized = true);
    }

    public void Initialize(string sentence)
    {
        _rectTransform = GetComponent<RectTransform>();

        textboxAnimSequence?.Kill();
        textboxAnimSequence = DOTween.Sequence();

        if (!_hasInitialized)
        {
            textboxAnimSequence.AppendInterval(0.5f);
            textboxAnimSequence.Append(_rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        }

        textboxAnimSequence.AppendCallback(() => _dialogueText.ShowText(sentence));
    }
}
    
    
