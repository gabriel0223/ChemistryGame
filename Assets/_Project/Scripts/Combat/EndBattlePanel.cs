using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndBattlePanel : MonoBehaviour
{
    [SerializeField] private RectTransform _endBattleWindow;
    [SerializeField] private float _finalYPosition;

    private Image _panel;
    private float _targetAlpha;
    
    private void OnEnable()
    {
        _panel = GetComponent<Image>();
        _targetAlpha = _panel.color.a;
        _panel.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0);
        
        _panel.DOFade(_targetAlpha, 1f);
        _endBattleWindow.DOAnchorPosY(_finalYPosition, 1f).OnComplete(HandlePosYTweenEnd);
    }

    public virtual void Initialize()
    {
        gameObject.SetActive(true);
    }

    public virtual void HandlePosYTweenEnd()
    {
        
    }
}
