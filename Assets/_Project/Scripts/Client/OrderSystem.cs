using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{
    [SerializeField] private Image _clientImage;
    [SerializeField] private TextMeshProUGUI _firstOrder;
    [SerializeField] private TextMeshProUGUI _secondOrder;
    [SerializeField] private Image _arrowFirstOrder;
    [SerializeField] private Image _arrowSecondOrder;

    public void ChangeClientPanel(Sprite clientImage, string firstOrder, string secondOrder, int arrowFirstOrderAngle, int arrowSecondOrderAngle)
    {
        this._clientImage.sprite = clientImage;
        this._firstOrder.text = firstOrder;
        this._secondOrder.text = secondOrder;
        this._arrowFirstOrder.transform.rotation = Quaternion.Euler(0, 0, arrowFirstOrderAngle);
        this._arrowSecondOrder.transform.rotation = Quaternion.Euler(0, 0, arrowSecondOrderAngle);
    }
}