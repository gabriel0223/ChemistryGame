using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DuelistVisualData", menuName = "Duelist Visual Data")]
public class DuelistVisualData : ScriptableObject
{
    public GameObject[] DuelistFacesPrefabs;
    public DuelistEyes[] DuelistEyes;
    public DuelistMouth[] DuelistMouths;
    public Sprite[] Noses;
    public Sprite[] Bodies;
    public Sprite[] Accessories;
}
