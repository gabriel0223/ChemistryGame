using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDataDisplayer : MonoBehaviour
{
    [SerializeField] private Image _cardBaseColor;
    [SerializeField] private TMP_Text _abbreviation;
    [SerializeField] private TMP_Text _elementName;
    [SerializeField] private TMP_Text _elementGroup;
    [SerializeField] private TMP_Text _atomicNumber;
    [SerializeField] private TMP_Text _electronegativity;

    public void SetCardColor(Sprite color)
    {
        _cardBaseColor.sprite = color;
    }

    public void InitializeDataDisplay(Element element, GameDifficulty difficulty)
    {
        if (difficulty == GameDifficulty.Normal)
        {
            _abbreviation.SetText(element.ElementData.Abbreviation);
            _elementName.SetText(element.ElementData.Elementname);
            _elementGroup.SetText(GetGroupName(element.ElementData.GROUP));
            _atomicNumber.SetText(element.ElementData.Atomicnumber.ToString());
            _electronegativity.SetText(element.ElementData.Electronegativity.ToString(CultureInfo.InvariantCulture));
        }
        else
        {
            _abbreviation.SetText(element.ElementData.Abbreviation);
            _elementName.SetText(element.ElementData.Elementname);
            _elementGroup.SetText(GetGroupName(element.ElementData.GROUP));
            _atomicNumber.SetText("");
            _electronegativity.SetText("");
        }
    }

    private string GetGroupName(Group group)
    {
        switch (group)
        {
            case Group.Ametais:
                return "Ametal";
                break;
            case Group.Metais_Alcalinos:
                return "Metal Alcalino";
                break;
            case Group.Metais_Alcalinos_Terrosos:
                return "Metal Alcalino Terroso";
                break;
            case Group.Metais_de_Transicao:
                return "Metal de Transição";
                break;
            case Group.Lantanideos:
                return "Lantanídeo";
                break;
            case Group.Actinideos:
                return "Actinídeo";
                break;
            case Group.Semimetais:
                return "Semimetal";
                break;
            case Group.Outros_Metais:
                return "Outro Metal";
                break;
            case Group.Halogenios:
                return "Halogênio";
                break;
            case Group.Gases_Nobres:
                return "Gás Nobre";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(@group), @group, null);
        }
    }
}
