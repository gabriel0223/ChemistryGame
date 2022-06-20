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
    [SerializeField] private GameObject _speakerPointer;

    private RectTransform _rectTransform;
    private bool _hasInitialized;
    private Sequence _textboxAnimSequence;
    private const float InitializationTime = 1f;

    public TextAnimatorPlayer DialogueText => _dialogueText;
    public bool HasInitialized => _hasInitialized;

    private void Start()
    {
        _rectTransform.localScale = Vector3.zero;
        _dialogueText.ShowText("");
        DOVirtual.DelayedCall( InitializationTime,() => _hasInitialized = true);
    }

    public void Initialize(string sentence, bool hasSpeakerPointer)
    {
        _rectTransform = GetComponent<RectTransform>();
        _speakerPointer.SetActive(hasSpeakerPointer);

        _textboxAnimSequence?.Kill();
        _textboxAnimSequence = DOTween.Sequence();

        if (!_hasInitialized)
        {
            _textboxAnimSequence.AppendInterval(0.5f);
            _textboxAnimSequence.Append(_rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        }

        _textboxAnimSequence.AppendCallback(() => _dialogueText.ShowText(sentence));
    }

    public void Close()
    {
        _textboxAnimSequence?.Kill();

        _rectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack)
            .OnComplete(() => Destroy(gameObject));
    }
}
    
    
