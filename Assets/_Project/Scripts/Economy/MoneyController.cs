using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private GameObject _moneyFeedbackPrefab;
    [SerializeField] private Color _goodMoneyColor;
    [SerializeField] private Color _okayMoneyColor;
    [SerializeField] private Color _badMoneyColor;
    [SerializeField] private TMP_Text _moneyText;
    public int MoneyObtained { get; private set; }
    
    private void Start()
    {
        ClientController.OnGiveGold += AddMoney;
    }

    private void OnDestroy()
    {
        ClientController.OnGiveGold -= AddMoney;
    }

    private void AddMoney(int money)
    {
        MoneyObtained += money;
        _moneyText.SetText("$" + MoneyObtained);
        
        PlayMoneyFeedback(money);
    }

    private void PlayMoneyFeedback(int money)
    {
        MoneyFeedback moneyFeedback = Instantiate(_moneyFeedbackPrefab, transform.root).GetComponent<MoneyFeedback>();
        Color moneyFeedbackColor;

        if (money > 6)
        {
            moneyFeedbackColor = _goodMoneyColor;
        }
        else if (money > 3)
        {
            moneyFeedbackColor = _okayMoneyColor;
        }
        else
        {
            moneyFeedbackColor = _badMoneyColor;
        }
        
        moneyFeedback.Initialize(money, moneyFeedbackColor);
    }
}
