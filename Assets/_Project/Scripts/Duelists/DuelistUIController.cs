using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuelistUIController : MonoBehaviour
{
    [SerializeField] private RectTransform _retractableShield;
    [SerializeField] private CanvasGroup _shieldOnGroup;
    [SerializeField] private TMP_Text _shieldPowerText;
    [SerializeField] private GameObject _attackBalloon;
    [SerializeField] private GameObject _defenseBalloon;
    [SerializeField] private GameObject[] _impactSpeechBubbles;

    private DuelistController _duelistController;
    private DuelistAI _duelistAI;
    private ShieldController _shieldController;
    private RectTransform _duelistVisualContainer;
    private Vector2 _shieldPosition;

    private void Awake()
    {
        _shieldController = GetComponent<ShieldController>();
        _duelistController = GetComponent<DuelistController>();
        _duelistAI = GetComponent<DuelistAI>();

        _shieldPosition = _retractableShield.anchoredPosition;
        _retractableShield.anchoredPosition = new Vector2(0, _shieldPosition.y);
        _duelistVisualContainer = GetComponentInParent<DuelistSpawner>().DuelistVisualContainer;
    }

    private void OnEnable()
    {
        _duelistController.OnEnableDefense += EnableShield;
        _duelistController.OnDisableDefense += DisableShield;
        _duelistController.OnTakeDamage += SpawnImpactSpeechBubble;

        _duelistAI.OnMovePlanned += DisplayDuelistBalloon;
    }

    private void OnDisable()
    {
        _duelistController.OnEnableDefense -= EnableShield;
        _duelistController.OnDisableDefense -= DisableShield;
        _duelistController.OnTakeDamage -= SpawnImpactSpeechBubble;
        
        _duelistAI.OnMovePlanned -= DisplayDuelistBalloon;
    }

    private void EnableShield()
    {
        _retractableShield.DOAnchorPosX(_shieldPosition.x, 0.5f).SetEase(Ease.OutBack);
        _shieldPowerText.SetText(_shieldController.GetPower().ToString());
        _shieldOnGroup.DOFade(1, 0.5f);
    }

    private void DisableShield()
    {
        _retractableShield.DOAnchorPosX(0, 0.5f).SetEase(Ease.InBack);
        _shieldOnGroup.DOFade(0, 0.5f);
    }

    private void DisplayDuelistBalloon(KeyValuePair<ActionType, int> plannedMovement)
    {
        if (plannedMovement.Key == ActionType.Attack)
        {
            DuelistBalloon balloon = Instantiate(_attackBalloon, _duelistVisualContainer).GetComponent<DuelistBalloon>();
            balloon.DisplayMovementInfo(plannedMovement.Value);
            _duelistAI.OnEnemyAttack += balloon.HideMovementInfo;
            _duelistAI.OnEnemyDefend += balloon.HideMovementInfo;
        }
        else
        {
            DuelistBalloon balloon = Instantiate(_defenseBalloon, _duelistVisualContainer).GetComponent<DuelistBalloon>();
            balloon.DisplayMovementInfo(plannedMovement.Value);
            _duelistAI.OnEnemyAttack += balloon.HideMovementInfo;
            _duelistAI.OnEnemyDefend += balloon.HideMovementInfo;
        }
    }

    private void SpawnImpactSpeechBubble()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-150, 150), Random.Range(-100, 100));
        RectTransform speechBubble = Instantiate(_impactSpeechBubbles[Random.Range(0, _impactSpeechBubbles.Length)], _duelistVisualContainer).GetComponent<RectTransform>();
        speechBubble.anchoredPosition = randomPosition;
    }
}
