using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelView : MonoBehaviour
{
    public event Action OnTutorialPanelClicked;
    
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() => OnTutorialPanelClicked?.Invoke());
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
