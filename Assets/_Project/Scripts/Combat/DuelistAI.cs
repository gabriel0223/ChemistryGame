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

    private DuelistData _duelistData;
    private DuelistController _duelistController;
    private WeaponController _weapon;
    private ShieldController _shield;
    private KeyValuePair<ActionType, int> _nextMove;
    private KeyValuePair<ActionType, int> _nextUpgrade;
    
    private const float MakeMoveDelay = 1f;

    private void Awake()
    {
        _duelistData = GamePersistentData.Instance.CurrentLevelData.DuelistData;
        
        _duelistController = GetComponent<DuelistController>();
        _weapon = GetComponent<WeaponController>();
        _shield = GetComponent<ShieldController>();
        
        _weapon.Initialize(_duelistData.Power);
        _shield.Initialize(_duelistData.Power);
    }

    public void PlanMove()
    {
        float odds = Random.Range(0f, 1f);

        ActionType upgradeType = odds <= 0.75f ? ActionType.Attack : ActionType.Defend;
        int powerToBeAdded = Mathf.RoundToInt(Random.Range(_duelistData.UpgradePowerRate / 2, _duelistData.UpgradePowerRate));

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
