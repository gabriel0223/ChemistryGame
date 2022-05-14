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
    public event Action OnStartEnemyTurn;
    
    private DuelistController _duelistController;
    private const float MakeMoveDelay = 1f;
    
    private void Awake()
    {
        _duelistController = GetComponent<DuelistController>();
    }

    public void MakeAMove()
    {
        OnStartEnemyTurn?.Invoke();
        
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.AppendInterval(MakeMoveDelay);
        moveSequence.AppendCallback(DecideMove);

        void DecideMove()
        {
            float odds = Random.Range(0f, 1f);
            
            if (odds <= 0.8f)
            {
                OnEnemyAttack?.Invoke();
            }
            else
            {
                OnEnemyDefend?.Invoke();
            }
        }
    }
}
