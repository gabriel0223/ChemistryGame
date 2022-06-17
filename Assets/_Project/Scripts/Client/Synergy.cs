using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy
{
    public string Name;
    public Color Color;
    public float Multiplier;

    public Synergy(string name, Color color, float multiplier)
    {
        Name = name;
        Color = color;
        Multiplier = multiplier;
    }
}
