using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuelConfirmationBox : MonoBehaviour
{
    [SerializeField] private Image _darkPanel;
    [SerializeField] private Transform _confirmationBox;
    [SerializeField] private TMP_Text _duelTitleText;
    [SerializeField] private TMP_Text _duelistName;
    [SerializeField] private Button _playButton;

    private bool _isConfirmationBoxOpen;
    private const float OpenPanelAlpha = 0.4f;
    private PlanetData _selectedPlanetData;
    private GamePersistentData _gamePersistentData;

    private void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;
    }

    public void OpenBox(PlanetData planetData)
    {
        _selectedPlanetData = planetData;

        _darkPanel.DOFade(0, 0);
        _darkPanel.DOFade(OpenPanelAlpha, 0.5f);
        
        _duelistName.SetText(planetData.DuelistData.Name);
        _duelTitleText.SetText($"Duelo {planetData.PlanetNumber}");
        _confirmationBox.localScale = Vector3.zero;
        _confirmationBox.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        gameObject.SetActive(true);
        
        _playButton.onClick.AddListener(StartDuel);
        
        _isConfirmationBoxOpen = true;
    }

    private void StartDuel()
    {
        if (!_gamePersistentData.IsPlayingFirstTime)
        {
            _gamePersistentData.CurrentLevelData = _selectedPlanetData;
        }

        LevelManager.Instance.GoToGameScene();
    }

    public void CloseBox()
    {
        if (!_isConfirmationBoxOpen)
        {
            return;
        }

        _darkPanel.DOFade(0, 0.5f);
        _confirmationBox.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
        _playButton.onClick.RemoveAllListeners();
        _isConfirmationBoxOpen = false;
    }
}
