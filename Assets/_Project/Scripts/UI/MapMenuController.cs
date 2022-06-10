using System.Collections;
using System.Collections.Generic;
using TMPro;
using Triplano.SaveSystem;
using UnityEngine;

public class MapMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        _moneyText.SetText(SaveSystem.Data.Money.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
