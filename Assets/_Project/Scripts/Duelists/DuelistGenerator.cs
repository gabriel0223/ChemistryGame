using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DuelistGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform _duelistVisualContainer;
    [SerializeField] private GameObject _duelistPrefab;
    [SerializeField] private GameObject[] _duelistFacesPrefabs;
    [SerializeField] private DuelistEyes[] _duelistEyes;
    [SerializeField] private DuelistMouth[] _duelistMouths;
    [SerializeField] private Sprite[] _noses;
    [SerializeField] private Sprite[] _bodies;

    private DuelistController _duelistGenerated;

    public RectTransform DuelistVisualContainer => _duelistVisualContainer;

    public DuelistController GenerateDuelist()
    {
        GameObject randomFace = _duelistFacesPrefabs[Random.Range(0, _duelistFacesPrefabs.Length)];
        DuelistEyes randomEyes = _duelistEyes[Random.Range(0, _duelistEyes.Length)];
        DuelistMouth randomMouth = _duelistMouths[Random.Range(0, _duelistMouths.Length)];
        Sprite randomNose = _noses[Random.Range(0, _noses.Length)];
        Sprite randomBody = _bodies[Random.Range(0, _bodies.Length)];
        float randomHue = Random.Range(0, 360);
        
        _duelistGenerated = Instantiate(_duelistPrefab, transform).GetComponent<DuelistController>();
        _duelistGenerated.SetDuelistName(DuelistNameGenerator.GenerateDuelistName());
        
        DuelistAnimation duelistAnimation = Instantiate(randomFace, _duelistVisualContainer).GetComponent<DuelistAnimation>();
        duelistAnimation.SetVisualFeatures(randomEyes, randomNose, randomMouth, randomBody);
        duelistAnimation.SetNewHue(randomHue);
        duelistAnimation.SetDuelistController(_duelistGenerated);

        return _duelistGenerated;
    }
}
