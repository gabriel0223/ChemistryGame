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
    [SerializeField] private Color _metaisDeTransicaoColor;
    [SerializeField] private Color _lantanideosColor;
    [SerializeField] private Color _actinideosColor;
    [SerializeField] private Color _outrosMetaisColor;
    public Synergy GetSynergy(Group group)
    {
        switch (group)
        {
            case Group.Ametais:
                return new Synergy("Ametais", _ametalColor, 2);
                break;
            case Group.Metais_Alcalinos:
                return new Synergy("Metais Alcalinos", _metaisAlcalinosColor, 2);
                break;
            case Group.Metais_Alcalinos_Terrosos:
                return new Synergy("Metais Alcalinos Terrosos", _metaisAlcalinosTerrososColor, 2);
                break;
            case Group.Metais_de_Transicao:
                return new Synergy("Metais de Transição", _metaisDeTransicaoColor, 2);
                break;
            case Group.Lantanideos:
                return new Synergy("Metais Alcalinos", _lantanideosColor, 2);
                break;
            case Group.Actinideos:
                return new Synergy("Actinídeos", _actinideosColor, 2);
                break;
            case Group.Semimetais:
                return new Synergy("Semimetais", _semimetaisColor, 2);
                break;
            case Group.Outros_Metais:
                return new Synergy("Outros Metais", _outrosMetaisColor, 2);
                break;
            case Group.Halogenios:
                return new Synergy("Halogênios", _halogeniosColor, 2);
                break;
            case Group.Gases_Nobres:
                return new Synergy("Gases Nobres", _gasesNobresColor, 2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(@group), @group, null);
        }
    }
}
