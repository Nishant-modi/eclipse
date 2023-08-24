using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Image healthImage;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthImage.fillAmount = health / 100f;

        if(health <= 0)
        {
            print("game over");
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthImage.fillAmount = health / 100f;

        if (health <= 0)
        {
            print("game over");
        }
    }

}
