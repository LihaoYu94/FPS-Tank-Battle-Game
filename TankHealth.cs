using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public int      startingHealth    = 100;
    public TextMesh displayArea;

    private float   currentHealth;  
    private bool    isDead;            
    
    private void OnEnable() {
        currentHealth = startingHealth;
        isDead        = false;
        SetHealthUI();
    }

    public void TakeDamage(float amount) {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        currentHealth = Mathf.Max(0, currentHealth - Mathf.Floor(amount));
        SetHealthUI();

        if (currentHealth <= 0 && ( ! isDead)) {
            OnDeath();
        }
    }
     
    private void SetHealthUI() {
        if (currentHealth <= 0) {
            displayArea.text = "DEAD";
        } else {
            displayArea.text = string.Format("HP: {0}", currentHealth);
        }
    }

    private void OnDeath() {
        isDead = true;
    }
}