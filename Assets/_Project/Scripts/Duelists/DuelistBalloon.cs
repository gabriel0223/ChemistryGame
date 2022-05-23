using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DuelistBalloon : MonoBehaviour
{
    [SerializeField] private TMP_Text _powerText;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Ease _easeIn;
    [SerializeField] private Ease _easeOut;
    
    public void DisplayMovementInfo(int power)
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, _animationDuration).SetEase(_easeIn);
        
        _powerText.SetText(power.ToString());
    }

    public void HideMovementInfo()
    {
        transform.DOScale(Vector3.zero, _animationDuration).SetEase(_easeOut).OnComplete(() => gameObject.SetActive(false));
    }
}
