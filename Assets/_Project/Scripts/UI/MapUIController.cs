using System.Collections;
using System.Collections.Generic;
using TMPro;
using Triplano.SaveSystem;
using UnityEngine;

public class MapUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    private GamePersistentData _gamePersistentData;
    
    // Start is called before the first frame update
    void Start()
    {
        _gamePersistentData = GamePersistentData.Instance;
        
        _moneyText.SetText(_gamePersistentData.PlayerMoney.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
