using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DuelistNameGenerator
{
    private static readonly string[] Titles =
    {
        "Destruidor",
        "Cachaceiro",
        "Sagaz",
        "Cruel",
        "Astuto",
        "Impiedoso"
    };

    private static readonly string[] Names =
    {
        "Blorg",
        "Robson",
        "Zurg",
        "Vanderlei",
        "Zolmit",
        "Cláudio"
    };

    public static string GenerateDuelistName()
    {
        string randomTitle = Titles[Random.Range(0, Titles.Length)];
        string randomName = Names[Random.Range(0, Names.Length)];

        return $"{randomName}, o {randomTitle}";
    }
}
