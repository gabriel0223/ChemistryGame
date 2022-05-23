using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CreditsScreenController : MonoBehaviour
{
    [SerializeField] private Transform _creditsPanel;

    private bool _isCreditsPanelOpen;
    
    public void OpenCredits()
    {
        if (_isCreditsPanelOpen)
        {
            return;
        }
        
        _creditsPanel.localScale = Vector3.zero;
        
        gameObject.SetActive(true);

        _creditsPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _isCreditsPanelOpen = true;
    }
    
    public void CloseCredits()
    {
        if (!_isCreditsPanelOpen)
        {
            return;
        }

        _creditsPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(true));
        _isCreditsPanelOpen = false;
    }
}
