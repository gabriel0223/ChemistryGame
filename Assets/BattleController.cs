using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn
}

public class BattleController : MonoBehaviour
{
    public event Action OnPlayerTurn;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private DuelistGenerator _duelistGenerator;
    [SerializeField] private TurnInformationPanel _playerTurnAnimation;
    [SerializeField] private TurnInformationPanel _enemyTurnAnimation;

    private DuelistController _playerDuelist;
    private PlayerController _playerController;
    private DuelistController _enemyDuelist;
    private DuelistAI _enemyAI;
    private BattleState _battleState = BattleState.PlayerTurn;

    void Start()
    {
        _playerDuelist = FindObjectOfType<DuelistController>();
        _playerController = _playerDuelist.GetComponent<PlayerController>();

        _enemyDuelist = _duelistGenerator.GenerateDuelist();
        _enemyAI = _enemyDuelist.GetComponent<DuelistAI>();

        _playerController.OnPlayerAttack += PlayerAttack;
        _enemyAI.OnEnemyAttack += EnemyAttack;
    }

    private void OnDisable()
    {
        _playerController.OnPlayerAttack -= PlayerAttack;
        _enemyAI.OnEnemyAttack -= EnemyAttack;
    }

    private void PlayerAttack()
    {
        _playerDuelist.Attack(_enemyDuelist);

        StartCoroutine(ChangeTurn());
    }

    private void EnemyAttack()
    {
        _enemyDuelist.Attack(_playerDuelist);
        
        StartCoroutine(ChangeTurn());
    }

    private IEnumerator ChangeTurn()
    {
        _battleState = _battleState == BattleState.PlayerTurn ? BattleState.EnemyTurn : BattleState.PlayerTurn;
        
        TurnInformationPanel turnInformationPanel = _battleState == BattleState.PlayerTurn ? _playerTurnAnimation : _enemyTurnAnimation;

        yield return new WaitForSeconds(1f);
        
        Instantiate(turnInformationPanel, _canvas.transform);

        yield return new WaitForSeconds(turnInformationPanel.GetAnimationDuration());
        
        if (_battleState == BattleState.EnemyTurn)
        {
            _enemyAI.MakeAMove();
        }
        else
        {
            _playerController.SetMyTurn(true);
            OnPlayerTurn?.Invoke();
        }
    }
}
