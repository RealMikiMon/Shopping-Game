using UnityEngine;
using System;
using System.Collections.Generic;

public enum Language
{
    English,
    Spanish,
    Catalan
}

public class LanguageData
{
    public Dictionary<string, string> Texts = new Dictionary<string, string>();
}


public class Localizer : MonoBehaviour
{
    public static Localizer Instance;

    public TextAsset DataSheet; 

    Dictionary <string, LanguageData> Data; 

    private Language currentLanguage;
    public Language DefaultLanguage;

    public static Action OnLanguageChange;

    void LoadLanguageSheet()
    {
        string[] lines = DataSheet.text.Split(new char[]{ '\n'});
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines.Length >1) AddLanguageData(lines[i]);

        }
    }

    public void SetLanguage(Language lang)
    {
        currentLanguage = lang;
        OnLanguageChange?.Invoke();
    } 
   
}
