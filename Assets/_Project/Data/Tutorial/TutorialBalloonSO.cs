using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New TutorialBalloon", menuName = "Tutorial Balloon")]
public class TutorialBalloonSO : ScriptableObject
{
    [TextArea(5, 10)]
    public string Sentence;

    public bool HasSpeakerPointer = true;
    public bool Skippable = true;
    public bool RequiresRespawn;
    public Vector2 CustomPosition;
}
