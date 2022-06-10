using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private CreditsScreenController _creditsScreen;

    public void Start()
    {
        AudioManager.instance.Play("title");
    }

    public void Play()
    {
        AudioManager.instance.Stop("title");
        AudioManager.instance.Play("accept01");
        SceneManager.LoadScene((int)SceneIndexes.MAP);
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
