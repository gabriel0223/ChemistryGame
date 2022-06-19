using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _cardArea;

    [Space] 
    [SerializeField] private TutorialPage[] _tutorialPages;
    
    private GamePersistentData _gamePersistentData;
    private TextboxView _currentTextbox;
    private Transform _canvas;
    private int _tutorialPageIndex;

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
        if (!_currentTextbox.HasInitialized)
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
        if (_currentTextbox == null)
        {
            _currentTextbox = Instantiate(_textboxPrefab, _canvas);
        }
        
        string currentTutorialText = _tutorialPages[index].TutorialBalloon.Sentence;

        _currentTextbox.Initialize(currentTutorialText);
        _tutorialPages[index].TutorialEvent?.Invoke();
    }

    private void GoToNextTutorialPage()
    {
        _tutorialPageIndex++;
        
        GoToTutorialPage(_tutorialPageIndex);
    }
    
    public void FocusOnObject(GameObject objectToBeFocused)
    {
        _tutorialPanel.DOFade(0.5f, 0.5f);

        Canvas objectCanvas = objectToBeFocused.AddComponent<Canvas>();

        objectCanvas.overrideSorting = true;
        objectCanvas.sortingOrder = 10;
    }
}
