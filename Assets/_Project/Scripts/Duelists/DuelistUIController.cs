using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DuelistUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _shieldOnGroup;
    [SerializeField] private TMP_Text _shieldPowerText;

    private DuelistController _duelistController;
    private ShieldController _shieldController;

    private void Awake()
    {
        _shieldController = GetComponent<ShieldController>();
        _duelistController = GetComponent<DuelistController>();
    }

    private void OnEnable()
    {
        _duelistController.OnEnableDefense += EnableShield;
        _duelistController.OnDisableDefense += DisableShield;
    }

    private void OnDisable()
    {
        _duelistController.OnEnableDefense -= EnableShield;
        _duelistController.OnDisableDefense -= DisableShield;
    }

    private void EnableShield()
    {
        _shieldPowerText.SetText(_shieldController.GetPower().ToString());
        _shieldOnGroup.DOFade(1, 0.5f);
    }

    private void DisableShield()
    {
        _shieldOnGroup.DOFade(0, 0.5f);
    }
}
