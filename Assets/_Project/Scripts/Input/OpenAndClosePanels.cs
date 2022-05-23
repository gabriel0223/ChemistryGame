using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class OpenAndClosePanels : MonoBehaviour
{
    [SerializeField] private float timeToOpen;
    [SerializeField] private float timeToClose;
    [SerializeField] private CanvasGroup focusBackground;

    /*private void Start()
    {
        transform.localScale = Vector2.zero;
    }*/

    public void OpenPanel()
    {
        transform.LeanMoveLocal(new Vector2(0, 148), timeToClose).setEaseInOutBack();
        EnableFocusBackground();
    }
    
    public void ClosePanel()
    {
        transform.LeanMoveLocal(new Vector2(0, 2390), timeToOpen).setEaseInOutBack();
        DisableFocusBackground();
    }

    private void EnableFocusBackground()
    {
        focusBackground.LeanAlpha(1, 1);
    }
    
    private void DisableFocusBackground()
    {
        focusBackground.LeanAlpha(0, 1);
    }
}