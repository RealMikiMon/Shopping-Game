using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public int Health = 100;
    public TextMeshProUGUI HealthText;

    private void Start()
    {
        UpdateUI();
    }
    public void RestoreHealth(int amount)
    {
        Health += amount;
        UpdateUI();
    }

    public void LoseHealth(int amount)
    {
        Health -= amount;
        UpdateUI();

        if(Health <= 0)
        {
            Health = 0;
            UpdateUI();
            SceneHandler.Instance.ChangeScene();
        }
    }

    private void UpdateUI()
    {
        HealthText.text = "Health: " + Health.ToString();
    }

}
