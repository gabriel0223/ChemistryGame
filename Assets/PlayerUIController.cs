using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private DuelistController _duelistController;
    [SerializeField] private Image _shieldHologram;
    [SerializeField] private Image _damagePanel;
    
    private void OnEnable()
    {
        _duelistController.OnEnableDefense += EnableShield;
        _duelistController.OnDisableDefense += DisableShield;

        _duelistController.OnTakeDamage += PlayTakeDamageAnimation;
    }

    private void OnDisable()
    {
        _duelistController.OnEnableDefense -= EnableShield;
        _duelistController.OnDisableDefense -= DisableShield;
        
        _duelistController.OnTakeDamage -= PlayTakeDamageAnimation;
    }

    private void EnableShield()
    {
        _shieldHologram.DOFillAmount(1, 1f);
    }

    private void DisableShield()
    {
        _shieldHologram.DOFillAmount(0, 1f);
    }

    private void PlayTakeDamageAnimation()
    {
        Sequence damageSequence = DOTween.Sequence();
        damageSequence.Append(_damagePanel.DOColor(new Color(_damagePanel.color.r, _damagePanel.color.g, _damagePanel.color.b, 0.5f), 0.25f));
        damageSequence.AppendInterval(0.5f);
        damageSequence.Append(_damagePanel.DOColor(new Color(_damagePanel.color.r, _damagePanel.color.g, _damagePanel.color.b, 0f), 0.25f));
    }
}
