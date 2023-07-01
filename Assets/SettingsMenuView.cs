using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuView : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Toggle _vibrationToggle;
    [SerializeField] private Slider _vibrationSlider;
    
    [SerializeField] private Image _darkPanel;
    [SerializeField] private Transform _settingsWindow;

    private bool _isMenuOpen;

    private GamePersistentData _gamePersistentData;

    private void OnEnable()
    {
        _sfxToggle.onValueChanged.AddListener(HandleSfxToggleClick);
        _musicToggle.onValueChanged.AddListener(HandleMusicToggleClick);
        _vibrationToggle.onValueChanged.AddListener(HandleVibrationToggleClick);
    }

    private void OnDisable()
    {
        _sfxToggle.onValueChanged.RemoveAllListeners();
        _musicToggle.onValueChanged.RemoveAllListeners();
        _vibrationToggle.onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;
        
        LoadSavedSettings();
        OpenSettingsMenu();
    }

    public void OpenSettingsMenu()
    {
        if (_isMenuOpen)
        {
            return;
        }

        gameObject.SetActive(true);
        
        _darkPanel.DOFade(0, 0);
        _darkPanel.DOFade(0.4f, 0.5f);
        
        _settingsWindow.localScale = Vector3.zero;
        _settingsWindow.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        _isMenuOpen = true;
    }

    public void CloseSettingsMenu()
    {
        _gamePersistentData.SavePlayerSettings();
        
        _darkPanel.DOFade(0, 0.5f);
        _settingsWindow.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
        
        _isMenuOpen = false;
    }

    private void LoadSavedSettings()
    {
        _sfxToggle.isOn = !_gamePersistentData.MuteSfx;
        _musicToggle.isOn = !_gamePersistentData.MuteMusic;
        _vibrationToggle.isOn = !_gamePersistentData.VibrationDisabled;
    }

    public void QuitButton()
    {
        if (AudioManager.instance.IfSoundIsPlaying("battle"))
        {
            AudioManager.instance.Stop("battle");
        }
        
        if (AudioManager.instance.IfSoundIsPlaying("briefing"))
        {
            AudioManager.instance.Stop("briefing");
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void HandleMusicToggleClick(bool toggleValue)
    {
        if (toggleValue)
        {
            _audioMixer.SetFloat("MusicVol", 0);
            _musicSlider.value = 1;
            _gamePersistentData.MuteMusic = false;
        }
        else
        {
            _audioMixer.SetFloat("MusicVol", -60);
            _musicSlider.value = 0;
            _gamePersistentData.MuteMusic = true;
        }
    }
    
    private void HandleSfxToggleClick(bool toggleValue)
    {
        if (toggleValue)
        {
            _audioMixer.SetFloat("SfxVol", 0);
            _sfxSlider.value = 1;
            _gamePersistentData.MuteSfx = false;
        }
        else
        {
            _audioMixer.SetFloat("SfxVol", -60);
            _sfxSlider.value = 0;
            _gamePersistentData.MuteSfx = true;
        }
    }
    
    private void HandleVibrationToggleClick(bool toggleValue)
    {
        if (toggleValue)
        {
            _vibrationSlider.value = 1;
            GamePersistentData.Instance.VibrationDisabled = false;
        }
        else
        {
            _vibrationSlider.value = 0;
            GamePersistentData.Instance.VibrationDisabled = true;
        }
    }
}
