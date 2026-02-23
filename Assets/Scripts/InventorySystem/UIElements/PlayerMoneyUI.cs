using TMPro;
using UnityEngine;

public class PlayerMoneyUI : MonoBehaviour
{
    public int Money = 100;
    public TextMeshProUGUI MoneyText;

    private void Start()
    {
        UpdateUI();
    }

    public bool CanAfford(int amount)
    {
        return Money >= amount;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        UpdateUI();
    }

    public void SpendMoney(int amount)
    {
        Money -= amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        MoneyText.text = Money.ToString();
    }
}
