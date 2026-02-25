using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.value = (int)Localizer.Instance.CurrentLanguage;
        dropdown.onValueChanged.AddListener(OnDropdownChange);
    }

    private void OnDropdownChange(int index)
    {
        Localizer.Instance.SetLanguage((Language)index);
    }
}

