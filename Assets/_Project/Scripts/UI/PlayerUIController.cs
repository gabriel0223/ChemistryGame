using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private DuelistController _duelistController;
    [SerializeField] private Image _playerMachine;
    [SerializeField] private Image _shieldHologram;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private Image _damagePanel;
    
    private void OnEnable()
    {
        _duelistController.OnEnableDefense += EnableShield;
        _duelistController.OnDisableDefense += DisableShield;

        _duelistController.OnShieldTakeDamage += HandleShieldTakeDamage;
        _duelistController.OnTakeDamage += PlayTakeDamageAnimation;
    }

    private void OnDisable()
    {
        _duelistController.OnEnableDefense -= EnableShield;
        _duelistController.OnDisableDefense -= DisableShield;

        _duelistController.OnShieldTakeDamage -= HandleShieldTakeDamage;
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
        #if !UNITY_WEBGL
        if (!GamePersistentData.Instance.VibrationDisabled)
        {
            Handheld.Vibrate();  
        }
        #endif

        Material mat = _playerMachine.GetComponent<Image>().material;

        Sequence damageSequence = DOTween.Sequence();
        damageSequence.Append(_damagePanel.DOColor(new Color(_damagePanel.color.r, _damagePanel.color.g, _damagePanel.color.b, 0.5f), 0.25f));
        damageSequence.Join(DOVirtual.Float(0, 0.2f, 0.5f, value => mat.SetFloat("_ChromAberrAmount", value)));
        damageSequence.AppendInterval(0.2f);
        damageSequence.Append(_damagePanel.DOColor(new Color(_damagePanel.color.r, _damagePanel.color.g, _damagePanel.color.b, 0f), 0.25f));
        damageSequence.Join(DOVirtual.Float(0.2f, 0, 0.5f, value => mat.SetFloat("_ChromAberrAmount", value)));
    }
    
    private void HandleShieldTakeDamage(bool shieldAbsorbedAllDamage)
    {
        if (shieldAbsorbedAllDamage)
        {
            _shieldHologram.transform.DOShakePosition(_shakeDuration, _shakeStrength);
        }
        else
        {
            DisableShield();   
        }
    }
}
