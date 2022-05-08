using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DuelistGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _duelistPrefab;
    [SerializeField] private GameObject[] _duelistFacesPrefabs;
    [SerializeField] private Sprite[] _eyes;
    [SerializeField] private Sprite[] _noses;
    [SerializeField] private Sprite[] _mouths;
    [SerializeField] private Sprite[] _bodies;

    private DuelistController _duelistGenerated;

    public DuelistController GenerateDuelist()
    {
        GameObject randomFace = _duelistFacesPrefabs[Random.Range(0, _duelistFacesPrefabs.Length)];
        Sprite randomEye = _eyes[Random.Range(0, _eyes.Length)];
        Sprite randomNose = _noses[Random.Range(0, _noses.Length)];
        Sprite randomMouth = _mouths[Random.Range(0, _mouths.Length)];
        Sprite randomBody = _bodies[Random.Range(0, _bodies.Length)];
        float randomHue = Random.Range(0, 360);
        
        _duelistGenerated = Instantiate(_duelistPrefab, transform).GetComponent<DuelistController>();
        _duelistGenerated.SetDuelistName(DuelistNameGenerator.GenerateDuelistName());
        
        DuelistAnimation duelistAnimation = Instantiate(randomFace, _duelistGenerated.transform).GetComponent<DuelistAnimation>();
        duelistAnimation.SetVisualFeatures(randomEye, randomNose, randomMouth, randomBody);
        duelistAnimation.SetNewHue(randomHue);

        return _duelistGenerated;
    }

    private Order GenerateOrder()
    {
        PropertyName property = (PropertyName)Random.Range(0, Enum.GetValues(typeof(PropertyName)).Length);
        PropertyQuantity quantity = (PropertyQuantity)Random.Range((int)PropertyQuantity.Minimum, (int)PropertyQuantity.Maximum);

        return new Order(new ElementProperty(property, quantity));
    }
}
