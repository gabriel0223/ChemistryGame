using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    [SerializeField] private Color _ametalColor;
    [SerializeField] private Color _gasesNobresColor;
    [SerializeField] private Color _metaisAlcalinosColor;
    [SerializeField] private Color _metaisAlcalinosTerrososColor;
    [SerializeField] private Color _semimetaisColor;
    [SerializeField] private Color _halogeniosColor;
    public Synergy GetSynergy(Group group)
    {
        switch (group)
        {
            case Group.Ametais:
                return new Synergy(_ametalColor, 2);
                break;
            case Group.Metais_Alcalinos:
                return new Synergy(_metaisAlcalinosColor, 2);
                break;
            case Group.Metais_Alcalinos_Terrosos:
                return new Synergy(_metaisAlcalinosTerrososColor, 2);
                break;
            case Group.Metais_de_Transicao:
                break;
            case Group.Lantanideos:
                break;
            case Group.Actinideos:
                break;
            case Group.Semimetais:
                return new Synergy(_semimetaisColor, 2);
                break;
            case Group.Outros_Metais:
                break;
            case Group.Halogenios:
                return new Synergy(_halogeniosColor, 2);
                break;
            case Group.Gases_Nobres:
                return new Synergy(_gasesNobresColor, 2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(@group), @group, null);
        }

        return new Synergy(_ametalColor, 2);
    }
}
