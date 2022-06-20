using System;
using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleZoom;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class TutorialPage
{
    public TutorialBalloonSO TutorialBalloon;
    public UnityEvent TutorialEvent;
}

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialPanelView _tutorialPanelView;
    [SerializeField] private Image _tutorialPanel;
    [SerializeField] private TextboxView _textboxPrefab;

    [Header("REFERENCES")]
    [SerializeField] private GameObject _cardArea;
    [SerializeField] private Equipment _weaponEquipment;
    [SerializeField] private Equipment _shieldEquipment;
    [SerializeField] private OpenAndClosePanels _periodicTablePanel;
    [SerializeField] private SimpleZoom _periodicTableZoom;
    [SerializeField] private RectTransform _periodicTableImage;
    
    [Space] 
    [SerializeField] private TutorialPage[] _tutorialPages;

    private GamePersistentData _gamePersistentData;
    private TextboxView _currentTextbox;
    private TutorialBalloonSO _currentTutorialBalloon;
    private Transform _canvas;
    private int _tutorialPageIndex;
    private int _equipmentsEnergized;

    private void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;
        _canvas = _cardArea.transform.root;

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

        if (_currentTutorialBalloon.RequiresRespawn)
        {
            _currentTextbox.Close();

            yield return new WaitForSeconds(0.25f);
            
            _currentTextbox = Instantiate(_textboxPrefab, _canvas);

            if (_currentTutorialBalloon.CustomPosition != Vector2.zero)
            {
                _currentTextbox.GetComponent<RectTransform>().anchoredPosition = _currentTutorialBalloon.CustomPosition;
            }
        }
        
        string currentTutorialText = _currentTutorialBalloon.Sentence;
        _currentTextbox.Initialize(currentTutorialText, _currentTutorialBalloon.HasSpeakerPointer);
    }

    private void GoToNextTutorialPage()
    {
        _tutorialPageIndex++;
        
        GoToTutorialPage(_tutorialPageIndex);
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
    }

    public void UnfocusObject(GameObject objectToBeUnfocused)
    {
        if (objectToBeUnfocused.GetComponent<GraphicRaycaster>() != null)
        {
            Destroy(objectToBeUnfocused.GetComponent<GraphicRaycaster>());
        }

        Destroy(objectToBeUnfocused.GetComponent<Canvas>());
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

    private void HandleEquipmentEnergized()
    {
        _equipmentsEnergized++;
        
        if (_equipmentsEnergized <= 1)
        {
            return;
        }
        
        GoToNextTutorialPage();
    }
}
