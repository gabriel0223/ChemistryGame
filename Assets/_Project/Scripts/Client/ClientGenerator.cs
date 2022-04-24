using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClientGenerator : MonoBehaviour
{
    [SerializeField] private CompoundSlot _compoundSlot;
    [SerializeField] private GameObject _clientPrefab;
    [SerializeField] private Sprite[] _clientImages;

    private ClientController _activeClient;

    private void OnEnable()
    {
        _compoundSlot.OnResetCompound += GenerateNewCustomer;
    }

    private void OnDisable()
    {
        _compoundSlot.OnResetCompound -= GenerateNewCustomer;
    }

    public void GenerateNewCustomer()
    {
        if (_activeClient != null)
        {
            Destroy(_activeClient.gameObject);
        }
        
        _activeClient = Instantiate(_clientPrefab, transform.parent).GetComponent<ClientController>();
        _activeClient.SetClientImage(_clientImages[Random.Range(0, _clientImages.Length)]);

        Order firstOrder = GenerateOrder();
        Order secondOrder = GenerateOrder();

        while (secondOrder.ElementProperty.PropertyName == firstOrder.ElementProperty.PropertyName)
        {
            secondOrder = GenerateOrder();
        }
        
        _activeClient.InitializeOrders(firstOrder, secondOrder);
        _activeClient.SetCompoundSlot(_compoundSlot);
    }

    private Order GenerateOrder()
    {
        PropertyName property = (PropertyName)Random.Range(0, Enum.GetValues(typeof(PropertyName)).Length);
        PropertyQuantity quantity = (PropertyQuantity)Random.Range((int)PropertyQuantity.Minimum, (int)PropertyQuantity.Maximum);

        while (quantity == 0)
        {
            quantity = (PropertyQuantity)Random.Range((int)PropertyQuantity.Minimum, (int)PropertyQuantity.Maximum);
        }

        return new Order(new ElementProperty(property, quantity));
    }
}
