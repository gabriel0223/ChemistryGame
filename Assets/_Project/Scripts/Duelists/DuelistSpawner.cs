using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DuelistSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _duelistVisualContainer;
    [SerializeField] private GameObject _duelistPrefab;
    [SerializeField] private GameObject[] _duelistFacesPrefabs;
    [SerializeField] private DuelistEyes[] _duelistEyes;
    [SerializeField] private DuelistMouth[] _duelistMouths;
    [SerializeField] private Sprite[] _noses;
    [SerializeField] private Sprite[] _bodies;
    [SerializeField] private Sprite[] _accessories;

    private DuelistController _duelistGenerated;

    public RectTransform DuelistVisualContainer => _duelistVisualContainer;

    public DuelistController SpawnDuelist(DuelistData duelistData)
    {
        GameObject face = _duelistFacesPrefabs[duelistData.FaceIndex];
        DuelistEyes eyes = _duelistEyes[duelistData.EyesIndex];
        DuelistMouth mouth = _duelistMouths[duelistData.MouthIndex];
        Sprite nose = _noses[duelistData.NoseIndex];
        Sprite body = _bodies[duelistData.BodyIndex];

        Sprite accessory = duelistData.AccessoryIndex < _accessories.Length? _accessories[duelistData.AccessoryIndex] : null;
        
        float hue = duelistData.HueColor;

        _duelistGenerated = Instantiate(_duelistPrefab, transform).GetComponent<DuelistController>();
        _duelistGenerated.Initialize(duelistData.Name, duelistData.Health);

        DuelistAnimation duelistAnimation = Instantiate(face, _duelistVisualContainer).GetComponent<DuelistAnimation>();
        duelistAnimation.SetVisualFeatures(eyes, nose, mouth, body, accessory);
        duelistAnimation.SetNewHue(hue);
        duelistAnimation.SetDuelistController(_duelistGenerated);

        return _duelistGenerated;
    }
}
