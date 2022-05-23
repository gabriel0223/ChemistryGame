using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLButton : MonoBehaviour
{
    [SerializeField] private string _urlToBeOpened;

    public void OpenURL()
    {
        Application.OpenURL(_urlToBeOpened);    
    }
}
