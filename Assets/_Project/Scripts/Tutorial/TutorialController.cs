using System;
using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleZoom;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class TutorialPage
{
    public TutorialBalloonSO TutorialBalloon;
    public UnityEvent TutorialEvent;
    public UnityEvent OnAllTextShowedEvent;
}

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialPanelView _tutorialPanelView;
    [SerializeField] private Image _tutorialPanel;
    [SerializeField] private TextboxView _textboxPrefab;

    [Header("REFERENCES")]
    [SerializeField] private GameObject _cardArea;
    [SerializeField] private BattleController _battleController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Equipment _weaponEquipment;
    [SerializeField] private Equipment _shieldEquipment;
    [SerializeField] private OpenAndClosePanels _periodicTablePanel;
    [SerializeField] private SimpleZoom _periodicTableZoom;
    [SerializeField] private RectTransform _periodicTableImage;
    [SerializeField] private GameObject _gameOverScreen;
    
    [Space] 
    [SerializeField] private TutorialPage[] _tutorialPages;
    
    private GamePersistentData _gamePersistentData;
    private TextboxView _currentTextbox;
    private TutorialBalloonSO _currentTutorialBalloon;
    private Transform _canvas;
    private int _tutorialPageIndex;
    private List<GameObject> _focusedObjects = new List<GameObject>();
    private int _equipmentsEnergized;
    private int _turnsPlayed;
    private DuelistAI _tutorialEnemyAI;
    private DuelistController _playerDuelist;

    private void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;
        _canvas = _cardArea.transform.root;
        _tutorialEnemyAI = _battleController.Enemy;
        _playerDuelist = _playerController.GetComponent<DuelistController>();

        if (!_gamePersistentData.IsPlayingFirstTime)
        {
            _tutorialPanel.raycastTarget = false;
            Destroy(gameObject);
            return;
        }

        _tutorialPanelView.OnTutorialPanelClicked += HandleTutorialPanelClicked;

        GoToTutorialPage(0);
    }

    private void OnDestroy()
    {
        _tutorialPanelView.OnTutorialPanelClicked -= HandleTutorialPanelClicked;
    }

    private void HandleTutorialPanelClicked()
    {
        if (_tutorialPageIndex >= _tutorialPages.Length)
        {
            return;
        }
        
        if (!_currentTextbox.HasInitialized || !_currentTutorialBalloon.Skippable)
        {
            return;
        }
        
        if (_currentTextbox.DialogueText.textAnimator.allLettersShown)
        {
            GoToNextTutorialPage();
        }
        else
        {
            _currentTextbox.DialogueText.SkipTypewriter();
        }
    }

    private void GoToTutorialPage(int index)
    {
        _currentTutorialBalloon = _tutorialPages[index].TutorialBalloon;

        StartCoroutine(InitializeTextbox());
        
        _tutorialPages[index].TutorialEvent?.Invoke();
        _tutorialPanel.raycastTarget = _currentTutorialBalloon.Skippable;
    }

    private IEnumerator InitializeTextbox()
    {
        if (_currentTextbox == null)
        {
            _currentTextbox = Instantiate(_textboxPrefab, _canvas);
        }
        else
        {
            if (_currentTutorialBalloon.RequiresRespawn)
            {
                _currentTextbox.Close();

                yield return new WaitForSeconds(0.25f);
            
                _currentTextbox = Instantiate(_textboxPrefab, _canvas);
            } 
        }
        
        if (_currentTutorialBalloon.CustomPosition != Vector2.zero)
        {
            _currentTextbox.GetComponent<RectTransform>().anchoredPosition = _currentTutorialBalloon.CustomPosition;
        }

        string currentTutorialText = _currentTutorialBalloon.Sentence;
        _currentTextbox.Initialize(currentTutorialText, _currentTutorialBalloon.HasSpeakerPointer);
        _currentTextbox.DialogueText.onTextShowed = _tutorialPages[_tutorialPageIndex].OnAllTextShowedEvent;
    }

    private void GoToNextTutorialPage()
    {
        _tutorialPageIndex++;
        
        GoToTutorialPage(_tutorialPageIndex);
    }

    public void CloseTextbox()
    {
        _currentTextbox.Close();
        //_currentTextbox = null;
    }
    
    public void FocusOnObject(GameObject objectToBeFocused)
    {
        _tutorialPanel.DOFade(0.7f, 0.5f);
        
        Canvas objectCanvas = objectToBeFocused.AddComponent<Canvas>();

        if (objectToBeFocused.GetComponent<Button>() != null)
        {
            objectToBeFocused.AddComponent<GraphicRaycaster>();
        }

        objectCanvas.overrideSorting = true;
        objectCanvas.sortingOrder = 10;
        
        _focusedObjects.Add(objectToBeFocused);
    }

    public void UnfocusObject(GameObject objectToBeUnfocused)
    {
        if (objectToBeUnfocused.GetComponent<GraphicRaycaster>() != null)
        {
            Destroy(objectToBeUnfocused.GetComponent<GraphicRaycaster>());
        }

        Destroy(objectToBeUnfocused.GetComponent<Canvas>());

        _focusedObjects.Remove(objectToBeUnfocused);
    }

    public void UnfocusAllObjects()
    {
        GameObject[] focusedObjects = _focusedObjects.ToArray();
        
        foreach (var focusedObject in focusedObjects)
        {
            UnfocusObject(focusedObject);
        }
    }
    
    public void DisableFocusPanel()
    {
        _tutorialPanel.DOFade(0f, 0.5f);
    }

    public void IncreaseObjectSortingOrder(GameObject selectedObject)
    {
        selectedObject.GetComponent<Canvas>().sortingOrder++;
    }

    public void SetDragAndDropInput(bool value)
    {
        foreach (var card in FindObjectsOfType<DragAndDrop>())
        {
            card.SetActive(value);
        }
    }

    public void StartPeriodicTableTutorial()
    {
        _periodicTablePanel.OnOpenPanel += HandlePeriodicTableOpened;
    }
    
    public void EndPeriodicTableTutorial()
    {
        _periodicTablePanel.OnClosePanel += HandlePeriodicTableClosed;
    }
    
    private void HandlePeriodicTableOpened()
    {
        _periodicTablePanel.OnOpenPanel -= HandlePeriodicTableOpened;
        
        _periodicTablePanel.GetComponent<Canvas>().sortingOrder *= 2;
        
        DOVirtual.DelayedCall(0.5f, GoToNextTutorialPage);
    }
    
    private void HandlePeriodicTableClosed()
    {
        _periodicTablePanel.OnClosePanel -= HandlePeriodicTableClosed;

        DOVirtual.DelayedCall(0.5f, GoToNextTutorialPage);
    }

    public void FocusOnPeriodicTableSummary()
    {
        _periodicTableImage.DOAnchorPos(new Vector2(1629, -1015), 1f);
        DOVirtual.Float(_periodicTableZoom.MinMaxZoom.min, 1.5f, 1f, value => _periodicTableZoom.SetZoom(value));
    }

    public void AddGraphicRaycastToObject(GameObject objectSelected)
    {
        objectSelected.AddComponent<GraphicRaycaster>();
    }

    public void StartEquipmentTutorial()
    {
        _weaponEquipment.OnEquipmentEnergized += HandleEquipmentEnergized;
        _shieldEquipment.OnEquipmentEnergized += HandleEquipmentEnergized;
    }

    public void StartActionTutorial()
    {
        _playerController.SetInputLocked(false);
        _playerController.OnPlayerAttack += HandlePlayerMadeFirstMove;
        _playerController.OnPlayerDefend += HandlePlayerMadeFirstMove;
    }

    private void HandlePlayerMadeFirstMove()
    {
        _playerController.OnPlayerAttack -= HandlePlayerMadeFirstMove;
        _playerController.OnPlayerDefend -= HandlePlayerMadeFirstMove;
        
        UnfocusAllObjects();
        DisableFocusPanel();
        CloseTextbox();
        StartBalloonTutorial();
    }

    private void HandleEquipmentEnergized()
    {
        _equipmentsEnergized++;
        
        if (_equipmentsEnergized <= 1)
        {
            return;
        }
        
        _weaponEquipment.OnEquipmentEnergized -= HandleEquipmentEnergized;
        _shieldEquipment.OnEquipmentEnergized -= HandleEquipmentEnergized;
        
        GoToNextTutorialPage();
    }

    private void StartBalloonTutorial()
    {
        _battleController.OnStartEnemyTurn += HandleFirstEnemyTurn;
        _battleController.OnStartEnemyTurn -= _tutorialEnemyAI.MakeAMove;
    }

    private void HandleFirstEnemyTurn()
    {
        GoToNextTutorialPage();
        
        _battleController.OnStartEnemyTurn -= HandleFirstEnemyTurn;
        _battleController.OnStartEnemyTurn += _tutorialEnemyAI.MakeAMove;
    }

    public void EndBalloonTutorial()
    {
        _tutorialPanelView.OnTutorialPanelClicked += HandleBalloonTutorialEnded;
        
        _tutorialPanelView.OnTutorialPanelClicked -= HandleTutorialPanelClicked;
    }

    private void HandleBalloonTutorialEnded()
    {
        _tutorialPanelView.OnTutorialPanelClicked -= HandleBalloonTutorialEnded;
        
        _tutorialPanelView.OnTutorialPanelClicked += HandleTutorialPanelClicked;
        
        _tutorialEnemyAI.MakeAMove();
        CloseTextbox();
        DisableFocusPanel();
        UnfocusAllObjects();

        _battleController.OnStartPlayerTurn += StartHpTutorial;
    }

    private void StartHpTutorial()
    {
        _battleController.OnStartPlayerTurn -= StartHpTutorial;
        
        GoToNextTutorialPage();
    }

    public void EndDurabilityTutorial()
    {
        _tutorialPanelView.OnTutorialPanelClicked += StartSynergyTutorial;
        
        _tutorialPanelView.OnTutorialPanelClicked -= HandleTutorialPanelClicked;
    }
    
    private void StartSynergyTutorial()
    {
        _tutorialPanelView.OnTutorialPanelClicked -= StartSynergyTutorial;
        
        _tutorialPanelView.OnTutorialPanelClicked += HandleTutorialPanelClicked;
        
        CloseTextbox();
        _tutorialPanel.raycastTarget = false;
        
        _battleController.OnStartPlayerTurn += HandleSynergyTutorial;
    }

    private void HandleSynergyTutorial()
    {
        _turnsPlayed++;

        if (_turnsPlayed <= 2)
        {
            return;
        }
        
        _battleController.OnStartPlayerTurn -= HandleSynergyTutorial;
        
        GoToNextTutorialPage();
    }

    public void EndSynergyTutorial()
    {
        _tutorialPanelView.OnTutorialPanelClicked += StartTutorialEnding;
        
        _tutorialPanelView.OnTutorialPanelClicked -= HandleTutorialPanelClicked;
    }

    private void StartTutorialEnding()
    {
        _tutorialPanelView.OnTutorialPanelClicked -= StartTutorialEnding;
        
        _tutorialPanelView.OnTutorialPanelClicked += HandleTutorialPanelClicked;
        
        CloseTextbox();
        _tutorialPanel.raycastTarget = false;
        _tutorialEnemyAI.GetComponent<DuelistController>().Weapon.AddPower(998);
        _tutorialEnemyAI.GetComponent<DuelistController>().Shield.AddPower(998);

        foreach (var actionBalloon in FindObjectsOfType<DuelistBalloon>())
        {
            actionBalloon.DisplayMovementInfo(999);
        }

        _playerController.GetComponent<DuelistController>().OnDie += HandleTutorialEnding;
        Destroy(_gameOverScreen);
    }

    private void HandleTutorialEnding()
    {
        _playerController.GetComponent<DuelistController>().OnDie -= HandleTutorialEnding;
        
        GoToNextTutorialPage();
    }

    public void EndTutorial()
    {
        _tutorialPanelView.OnTutorialPanelClicked += GoToMap;
        
        _tutorialPanelView.OnTutorialPanelClicked -= HandleTutorialPanelClicked;
    }

    private void GoToMap()
    {
        _tutorialPanelView.OnTutorialPanelClicked += HandleTutorialPanelClicked;
        
        _tutorialPanelView.OnTutorialPanelClicked -= GoToMap;
        
        _gamePersistentData.IsPlayingFirstTime = false;
        _gamePersistentData.SavePlayerData();
        
        SceneManager.LoadScene((int)SceneIndexes.MAP);
    }
}
