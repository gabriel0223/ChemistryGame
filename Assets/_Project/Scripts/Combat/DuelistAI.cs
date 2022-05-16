using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuelistAI : MonoBehaviour
{
    public event Action OnEnemyAttack;
    public event Action OnEnemyDefend;
    public event Action<KeyValuePair<ActionType, int>> OnMovePlanned;

    [SerializeField] private float _maxPowerToAdd;
    [SerializeField] private float _minPowerToAdd;
    
    private DuelistController _duelistController;
    private KeyValuePair<ActionType, int> _nextMove;
    private KeyValuePair<ActionType, int> _nextUpgrade;
    
    private const float MakeMoveDelay = 1f;
    
    private void Awake()
    {
        _duelistController = GetComponent<DuelistController>();
    }

    public void PlanMove()
    {
        float odds = Random.Range(0f, 1f);

        ActionType upgradeType = odds <= 0.65f ? ActionType.Attack : ActionType.Defend;
        int powerToBeAdded = Mathf.RoundToInt(Random.Range(_minPowerToAdd, _maxPowerToAdd));

        ActionType move = odds <= 0.8f ? ActionType.Attack : ActionType.Defend;
        int movePower = move == ActionType.Attack ? _duelistController.Weapon.Power : _duelistController.Shield.Power;
        
        _nextUpgrade = new KeyValuePair<ActionType, int>(upgradeType, powerToBeAdded);

        if (_nextUpgrade.Key == move)
        {
            movePower += powerToBeAdded;
        }
        
        _nextMove = new KeyValuePair<ActionType, int>(move, movePower);
        
        OnMovePlanned?.Invoke(_nextMove);
    }
    
    public void MakeAMove()
    {
        DecideUpgrade();
        
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.AppendInterval(MakeMoveDelay);
        moveSequence.AppendCallback(DecideMove);

        void DecideMove()
        {
            if (_nextMove.Key == ActionType.Attack)
            {
                OnEnemyAttack?.Invoke();
            }
            else
            {
                OnEnemyDefend?.Invoke();
            }
        }

        void DecideUpgrade()
        {
            if (_nextUpgrade.Key == ActionType.Attack)
            {
                UpgradeWeapon(_nextUpgrade.Value);
            }
            else
            {
                UpgradeShield(_nextUpgrade.Value);
            }
        }
    }

    private void UpgradeWeapon(int power)
    {
        _duelistController.Weapon.AddPower(power);
    }
    
    private void UpgradeShield(int power)
    {
        _duelistController.Shield.AddPower(power);
    }
}
