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
    [SerializeField] private DuelistBalloon _attackBalloon;
    [SerializeField] private DuelistBalloon _defenseBalloon;

    private DuelistController _duelistController;
    private DuelistAI _duelistAI;
    private ShieldController _shieldController;

    private void Awake()
    {
        _shieldController = GetComponent<ShieldController>();
        _duelistController = GetComponent<DuelistController>();
        _duelistAI = GetComponent<DuelistAI>();
    }

    private void OnEnable()
    {
        _duelistController.OnEnableDefense += EnableShield;
        _duelistController.OnDisableDefense += DisableShield;

        _duelistAI.OnMovePlanned += DisplayDuelistBalloon;

        _duelistAI.OnEnemyAttack += HideDuelistBalloon;
        _duelistAI.OnEnemyDefend += HideDuelistBalloon;
    }

    private void OnDisable()
    {
        _duelistController.OnEnableDefense -= EnableShield;
        _duelistController.OnDisableDefense -= DisableShield;
        
        _duelistAI.OnMovePlanned -= DisplayDuelistBalloon;
        
        _duelistAI.OnEnemyAttack -= HideDuelistBalloon;
        _duelistAI.OnEnemyDefend -= HideDuelistBalloon;
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

    private void DisplayDuelistBalloon(KeyValuePair<ActionType, int> plannedMovement)
    {
        if (plannedMovement.Key == ActionType.Attack)
        {
            _attackBalloon.DisplayMovementInfo(plannedMovement.Value);
        }
        else
        {
            _defenseBalloon.DisplayMovementInfo(plannedMovement.Value);
        }
    }

    private void HideDuelistBalloon()
    {
        if (_attackBalloon.gameObject.activeSelf)
        {
            _attackBalloon.HideMovementInfo();
        }
        else if (_defenseBalloon.gameObject.activeSelf)
        {
            _defenseBalloon.HideMovementInfo();
        }
    }
}
