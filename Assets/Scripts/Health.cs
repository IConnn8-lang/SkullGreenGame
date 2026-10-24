using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public float stdDmg = 10f;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
  
    }
    /*void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.tag.Equals("HitPoint"))
        {
            int diii;
            TakeDamage(stdDmg);
        }
       
    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.gameObject);
        if (collider.gameObject.tag.Equals("HitPoint"))
        {
            TakeDamage(stdDmg);
        }
        else if (collider.gameObject.tag.Equals("HitPointBoss"))
        {
            TakeDamage(stdDmg * 50);
        }
        else if (collider.gameObject.tag.Equals("HitPointGoblin"))
        {
            TakeDamage(stdDmg * 2);
        }
    }

    private void TakeDamage(float stdDmg)
    {
        currentHealth -= (int)stdDmg;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

}
