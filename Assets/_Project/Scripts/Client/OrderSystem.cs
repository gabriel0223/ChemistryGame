using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{
    private ClientGenerator _clientGenerator;

    private void Awake()
    {
        _clientGenerator = GetComponent<ClientGenerator>();
    }

    private void Start()
    {
        _clientGenerator.GenerateNewCustomer();
    }
}