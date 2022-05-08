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
    
    private DuelistController _duelistController;

    private void Awake()
    {
        _duelistController = GetComponent<DuelistController>();
    }

    public void MakeAMove()
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.AppendInterval(1f);
        moveSequence.AppendCallback(decideMove);

        void decideMove()
        {
            float odds = Random.Range(0f, 1f);
            
            if (odds <= 0.8f)
            {
                OnEnemyAttack?.Invoke();
            }
            else
            {
                OnEnemyAttack?.Invoke();
                //OnEnemyDefend?.Invoke();
            }
        }
    }
}
