using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Image _echoBar;
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _echoBarDelay;

    private int _initialHealth;

    private void Start()
    {
        _lifeText.SetText($"{_initialHealth}/{_initialHealth}");
    }

    public void SetInitialHealth(int health)
    {
        _initialHealth = health;
    }
    
    public void AnimateBar(int newHealth)
    {
        _bar.DOFillAmount((float)newHealth / _initialHealth, _animationDuration);
        _lifeText.SetText($"{newHealth}/{_initialHealth}");
        StartCoroutine(AnimateEchoBar(newHealth));
    }

    private IEnumerator AnimateEchoBar(int newHealth)
    {
        yield return new WaitForSeconds(_echoBarDelay);
        
        _echoBar.DOFillAmount((float)newHealth / _initialHealth, _animationDuration);
    }
}
