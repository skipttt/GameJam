using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public PlayerHUD hud;

    void Start()
    {
        currentHealth = maxHealth;
        hud.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        // Solo para prueba: bajar vida con tecla H
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hud.SetHealth(currentHealth, maxHealth);
    }
}
