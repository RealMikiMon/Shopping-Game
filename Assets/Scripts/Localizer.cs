using UnityEngine;
using System;
using System.Collections.Generic;

public enum Language
{
    English,
    Catalan,
    Spanish
}

public class LanguageData
{
    public Dictionary<string, string> Texts = new Dictionary<string, string>();
}

public class Localizer : MonoBehaviour
{
    public static Localizer Instance;

    public TextAsset DataSheet;

    private Dictionary<string, LanguageData> Data = new Dictionary<string, LanguageData>();

    private Language currentLanguage;
    public Language DefaultLanguage = Language.English;

    public static Action OnLanguageChange;
    public Language CurrentLanguage => currentLanguage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguageSheet();
            SetLanguage(DefaultLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void LoadLanguageSheet()
    {
        string[] lines = DataSheet.text.Split('\n');

        if (lines.Length < 2)
        {
            Debug.LogError("Localization sheet is empty or invalid.");
            return;
        }

        string[] headers = lines[0].Split(';');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            AddLanguageData(lines[i], headers);
        }
    }
    void AddLanguageData(string line, string[] headers)
    {
        string[] parts = line.Split(';');

        if (parts.Length < 2)
        {
            return;
        }
        string key = parts[0].Trim();

        if (!Data.ContainsKey(key))
        {
            Data[key] = new LanguageData();
        }

        for (int i = 1; i < parts.Length; i++)
        {
            string header = headers[i].Trim(); 
            string value = parts[i].Trim();

            if (!Enum.TryParse(header, out Language lang))
            {
                Debug.LogWarning($"Language '{header}' not found in enum.");
                continue;
            }

            Data[key].Texts[lang.ToString()] = value;
        }
    }
    public void SetLanguage(Language lang)
    {
        currentLanguage = lang;
        OnLanguageChange?.Invoke();
    }


    public string GetTextInternal(string key)
    {
        if (!Data.ContainsKey(key))
        {
            return $"[Missing key: {key}]";
        }
        string lang = currentLanguage.ToString();

        if (Data[key].Texts.ContainsKey(lang))
        {
            return Data[key].Texts[lang];
        }

        return $"[Missing {lang} for {key}]";
    }

    public static string GetText(string key)
    {
        return Instance != null ? Instance.GetTextInternal(key) : "[No Localizer Instance]";
    }
}