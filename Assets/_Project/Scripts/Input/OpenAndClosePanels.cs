using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class OpenAndClosePanels : MonoBehaviour
{
    public event Action OnOpenPanel;
    public event Action OnClosePanel;
    
    [SerializeField] private float timeToOpen;
    [SerializeField] private float timeToClose;
    [SerializeField] private CanvasGroup focusBackground;

    /*private void Start()
    {
        transform.localScale = Vector2.zero;
    }*/

    public void OpenPanel()
    {
        transform.LeanMoveLocal(new Vector2(0, 146), timeToClose).setEaseInOutBack();
        EnableFocusBackground();
        
        OnOpenPanel?.Invoke();
    }
    
    public void ClosePanel()
    {
        transform.LeanMoveLocal(new Vector2(0, 2390), timeToOpen).setEaseInOutBack();
        DisableFocusBackground();
        
        OnClosePanel?.Invoke();
    }

    private void EnableFocusBackground()
    {
        focusBackground.LeanAlpha(1, 0.5f);
    }
    
    private void DisableFocusBackground()
    {
        focusBackground.LeanAlpha(0, 0.5f);
    }
}