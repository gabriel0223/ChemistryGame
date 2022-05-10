using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyFeedback : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    private TMP_Text _moneyText; 
    
    public void Initialize(int value, Color color)
    {
        _moneyText = GetComponent<TMP_Text>();
        
        _moneyText.SetText("+$" + value);
        _moneyText.color = color;
        _moneyText.DOFade(0, _animationDuration);
        transform.DOMoveY(transform.position.y + 50, _animationDuration * 2);
        
        Destroy(gameObject, _animationDuration * 2);
    }
}
