using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    PlayerWon,
    PlayerLost
}

public class BattleController : MonoBehaviour
{
    public event Action OnStartBattle;
    public event Action OnStartPlayerTurn;
    public event Action OnEndPlayerTurn;
    public event Action OnStartEnemyTurn;
    public event Action OnEndEnemyTurn;
    public event Action OnEndTurn;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private DuelistGenerator _duelistGenerator;
    [SerializeField] private EndBattlePanel _winScreen;
    [SerializeField] private EndBattlePanel _gameOverScreen;
    [SerializeField] private TurnInformationPanel _playerTurnAnimation;
    [SerializeField] private TurnInformationPanel _enemyTurnAnimation;
    [SerializeField] private DuelistController _playerDuelist;

    private const float EndTurnDelay = 1f;
    private const float EndBattleDelay = 2f;
    
    private PlayerController _playerController;
    private DuelistController _enemyDuelist;
    private DuelistAI _enemyAI;
    private BattleState _battleState = BattleState.PlayerTurn;

    void Awake()
    {
        _playerController = _playerDuelist.GetComponent<PlayerController>();

        _enemyDuelist = _duelistGenerator.GenerateDuelist();
        _enemyAI = _enemyDuelist.GetComponent<DuelistAI>();
        
        _playerDuelist.SetOpponent(_enemyDuelist);
        _enemyDuelist.SetOpponent(_playerDuelist);

        OnStartPlayerTurn += _playerDuelist.StartTurn;
        OnStartPlayerTurn += _playerController.StartPlayerTurn;

        OnStartBattle += _enemyAI.PlanMove;
        OnStartPlayerTurn += _enemyAI.PlanMove;
        
        OnStartEnemyTurn += _enemyDuelist.StartTurn;
        OnStartEnemyTurn += _enemyAI.MakeAMove;

        _playerController.OnPlayerAttack += PlayerAttack;
        _playerController.OnPlayerDefend += PlayerDefend;
        _enemyAI.OnEnemyAttack += EnemyAttack;
        _enemyAI.OnEnemyDefend += EnemyDefend;
        
        _enemyDuelist.OnDie += PlayerWinsHandler;
        _playerDuelist.OnDie += PlayerLosesHandler;
    }

    private void Start()
    {
        OnStartBattle?.Invoke();
        AudioManager.instance.Play("battle");
    }

    private void OnDisable()
    {
        OnStartPlayerTurn -= _playerDuelist.StartTurn;
        OnStartPlayerTurn -= _playerController.StartPlayerTurn;
        
        OnStartBattle -= _enemyAI.PlanMove;
        OnStartPlayerTurn -= _enemyAI.PlanMove;
        
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

        AudioManager.instance.PlayRandomBetweenSounds(new[] { "shot01", "shot03" });

        StartCoroutine(EndTurn());
    }
    
    private void PlayerDefend()
    {
        _playerDuelist.Defend();

        AudioManager.instance.Play("shield01");

        StartCoroutine(EndTurn());
    }

    private void EnemyAttack()
    {
        _enemyDuelist.Attack(_playerDuelist);

        AudioManager.instance.PlayRandomBetweenSounds(new[] { "shot01", "shot03" });

        StartCoroutine(EndTurn());
    }
    
    private void EnemyDefend()
    {
        _enemyDuelist.Defend();

        AudioManager.instance.Play("shield01");

        StartCoroutine(EndTurn());
    }

    private void PlayerWinsHandler()
    {
        _battleState = BattleState.PlayerWon;

        StartCoroutine(EndBattle());
    }
    
    private void PlayerLosesHandler()
    {
        _battleState = BattleState.PlayerLost;

        StartCoroutine(EndBattle());
    }

    private IEnumerator EndTurn()
    {
        if (_battleState == BattleState.PlayerWon || _battleState == BattleState.PlayerLost)
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

        if (!AudioManager.instance.IfSoundIsPlaying("battle")) {
            AudioManager.instance.Play("battle");
        }
    }

    private IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(EndBattleDelay);

        if (_battleState == BattleState.PlayerLost)
        {
            _gameOverScreen.Initialize();
        }
        else if (_battleState == BattleState.PlayerWon)
        {
            _winScreen.Initialize();
        }
    }
}
