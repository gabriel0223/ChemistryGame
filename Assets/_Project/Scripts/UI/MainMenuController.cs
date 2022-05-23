using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private CreditsScreenController _creditsScreen;
    
    public void Play()
    {
        SceneManager.LoadScene((int)SceneIndexes.GAME);
    }

    public void OpenCredits()
    {
        _creditsScreen.OpenCredits();
    }

    public void CloseCredits()
    {
        _creditsScreen.CloseCredits();
    }
}
