using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private ClientGenerator _clientGenerator;

    private void Start()
    {
        _clientGenerator.GenerateNewCustomer();
    }
}
