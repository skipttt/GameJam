using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text ammoText;
    public GameObject outOfAmmoMessage;
    public Image healthBarFill; // La imagen que representa la barra de vida

    public void SetAmmo(int currentAmmo, int maxAmmo)
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + (maxAmmo == int.MaxValue ? "∞" : maxAmmo.ToString());
    }

    public void ShowOutOfAmmo(bool state)
    {
        if (outOfAmmoMessage != null)
            outOfAmmoMessage.SetActive(state);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}
