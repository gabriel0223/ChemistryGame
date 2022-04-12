using System;
using UnityEngine;

public class EventsButtom : MonoBehaviour
{
    public event Action OnSendOrder;
    public event Action OnChangeClient;

    public void ButtonPrepareOrder()
    {
        OnSendOrder = ChangeClient;
    }

    public void ButtonDeliverOrder()
    {
        OnSendOrder?.Invoke();
    }

    private void ChangeClient()
    {
        OnSendOrder = null;
        
        OnChangeClient?.Invoke();
    }
}