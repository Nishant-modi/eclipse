using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Image healthImage;
    public int health;
    public GameObject gameOverUI;
    public PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        pm = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthImage.fillAmount = health / 100f;

        if(health <= 0)
        {
            print("game over");
            pm.canMove = false;
            gameOverUI.SetActive(true);
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
