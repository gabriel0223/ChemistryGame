using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New TutorialBalloon", menuName = "Tutorial Balloon")]
public class TutorialBalloonSO : ScriptableObject
{
    [TextArea(5, 10)]
    public string Sentence;
}
