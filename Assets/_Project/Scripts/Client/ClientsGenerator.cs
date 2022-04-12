using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClientsGenerator : MonoBehaviour
{
    [SerializeField] private Image[] _clientImage;
    
    [SerializeField] private string[] _firstOrder;
    [SerializeField] private string[] _secondOrder;
    
    [SerializeField] private int[] _arrowFirstOrder;
    [SerializeField] private int[] _arrowSecondOrder;
    
    private OrderSystem _orderSystem;
    private EventsButtom _eventsButtom;

    public enum Clients
    {
        Client,
        Client2,
        Client3
    }

    public enum Order
    {
        FirstOrder,
        SecondOrder,
        ThirdOrder
    }

    public enum ArrowOrder
    {
        Up,
        Down
    }
    private void Awake()
    {
        _orderSystem = GetComponent<OrderSystem>();
        _eventsButtom = GetComponent<EventsButtom>();
    }
    
    private void OnEnable()
    {
        _eventsButtom.OnChangeClient += GenerateNewCustomer;
    }

    private void OnDisable()
    {
        _eventsButtom.OnChangeClient -= GenerateNewCustomer;
    }


    private void GenerateNewCustomer()
    {
        var randomClient = Random.Range(0, _clientImage.Length);                      //Clientes
        
        var randomFirstOrder = Random.Range(0, _firstOrder.Length);                   //Pedido Um
        var randomSecondOrder = Random.Range(0, _secondOrder.Length);                 //Pedido Dois

        var randomFirstArrowOrder = Random.Range(0, _arrowFirstOrder.Length);        //Flecha Um
        var randomSecondArrowOrder = Random.Range(0, _arrowSecondOrder.Length);      //Flecha Dois
        
        while (randomSecondOrder == randomFirstOrder)                                   //Não repita o primeiro pedido
        {
            randomSecondOrder = Random.Range(0, _secondOrder.Length);
        }
        
        _orderSystem.ChangeClientPanel(_clientImage[randomClient].sprite, _firstOrder[randomFirstOrder], _secondOrder[randomSecondOrder], _arrowFirstOrder[randomFirstArrowOrder], _arrowSecondOrder[randomSecondArrowOrder]);
    }
}
