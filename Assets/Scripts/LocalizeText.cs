using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))] 
public class LocalizeText: MonoBehaviour
{
    public string TextKey; 
    private Text _textValue;


    private void Awake()
    {
        _textValue = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Localizer.OnLanguageChange += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        Localizer.OnLanguageChange -= UpdateText;
    }

    private void UpdateText()
    {
        if (_textValue != null && !string.IsNullOrEmpty(TextKey))
        {
            _textValue.text = Localizer.GetText(TextKey);
        }
    }
}