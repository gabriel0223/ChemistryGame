using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    [SerializeField] private Color _ametalColor;

    public Synergy GetSynergy(Group group)
    {
        switch (group)
        {
            case Group.Ametais:
                return new Synergy(_ametalColor, 2);
                break;
            case Group.Metais_Alcalinos:
                break;
            case Group.Metais_Alcalinos_Terrosos:
                break;
            case Group.Metais_de_Transicao:
                break;
            case Group.Lantanideos:
                break;
            case Group.Actinideos:
                break;
            case Group.Semimetais:
                break;
            case Group.Outros_Metais:
                break;
            case Group.Halogenios:
                break;
            case Group.Gases_Nobres:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(@group), @group, null);
        }

        return new Synergy(_ametalColor, 2);
    }
}
