using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    End
}

public class BattleController : MonoBehaviour
{
    public event Action OnStartPlayerTurn;
    public event Action OnEndPlayerTurn;
    public event Action OnStartEnemyTurn;
    public event Action OnEndEnemyTurn;
    public event Action OnEndTurn;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private DuelistGenerator _duelistGenerator;
    [SerializeField] private EndBattlePanel _endBattlePanel;
    [SerializeField] private TurnInformationPanel _playerTurnAnimation;
    [SerializeField] private TurnInformationPanel _enemyTurnAnimation;

    private const float EndTurnDelay = 1f;
    private const float EndBattleDelay = 2f;
    
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

        OnStartPlayerTurn += _playerDuelist.StartTurn;
        OnStartPlayerTurn += _playerController.StartPlayerTurn;
        OnStartEnemyTurn += _enemyDuelist.StartTurn;
        OnStartEnemyTurn += _enemyAI.MakeAMove;

        _playerController.OnPlayerAttack += PlayerAttack;
        _playerController.OnPlayerDefend += PlayerDefend;
        _enemyAI.OnEnemyAttack += EnemyAttack;
        _enemyAI.OnEnemyDefend += EnemyDefend;
        
        _enemyDuelist.OnDie += PlayerWinsHandler;
        _playerDuelist.OnDie += PlayerLosesHandler;
    }

    private void OnDisable()
    {
        OnStartPlayerTurn -= _playerDuelist.StartTurn;
        OnStartPlayerTurn -= _playerController.StartPlayerTurn;
        OnStartEnemyTurn -= _enemyDuelist.StartTurn;
        OnStartEnemyTurn -= _enemyAI.MakeAMove;
        
        _playerController.OnPlayerAttack -= PlayerAttack;
        _playerController.OnPlayerDefend -= PlayerDefend;
        _enemyAI.OnEnemyAttack -= EnemyAttack;
        _enemyAI.OnEnemyDefend -= EnemyDefend;
        
        _enemyDuelist.OnDie -= PlayerWinsHandler;
        _playerDuelist.OnDie -= PlayerLosesHandler;
    }

    private void PlayerAttack()
    {
        _playerDuelist.Attack(_enemyDuelist);

        StartCoroutine(EndTurn());
    }
    
    private void PlayerDefend()
    {
        _playerDuelist.Defend();

        StartCoroutine(EndTurn());
    }

    private void EnemyAttack()
    {
        _enemyDuelist.Attack(_playerDuelist);
        
        StartCoroutine(EndTurn());
    }
    
    private void EnemyDefend()
    {
        _enemyDuelist.Defend();
        
        StartCoroutine(EndTurn());
    }

    private void PlayerWinsHandler()
    {
        _battleState = BattleState.End;

        StartCoroutine(EndBattle());
    }
    
    private void PlayerLosesHandler()
    {
        _battleState = BattleState.End;

        StartCoroutine(EndBattle());
    }

    private IEnumerator EndTurn()
    {
        if (_battleState == BattleState.End)
        {
            yield break;
        }
        
        _battleState = _battleState == BattleState.PlayerTurn ? BattleState.EnemyTurn : BattleState.PlayerTurn;

        if (_battleState == BattleState.EnemyTurn)
        {
            OnEndPlayerTurn?.Invoke();
        }
        else
        {
            OnEndEnemyTurn?.Invoke();
        }
        
        yield return new WaitForSeconds(EndTurnDelay);
        
        OnEndTurn?.Invoke();

        StartCoroutine(StartNewTurn());
    }

    private IEnumerator StartNewTurn()
    {
        TurnInformationPanel turnInformationPanel = _battleState == BattleState.PlayerTurn ? _playerTurnAnimation : _enemyTurnAnimation;
        
        Instantiate(turnInformationPanel, _canvas.transform);

        yield return new WaitForSeconds(turnInformationPanel.GetAnimationDuration());
        
        if (_battleState == BattleState.EnemyTurn)
        {
            OnStartEnemyTurn?.Invoke();
        }
        else
        {
            OnStartPlayerTurn?.Invoke();
        }
    }

    private IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(EndBattleDelay);
        
        _endBattlePanel.Initialize();
    }
}
