using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator anim;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt anim
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play die anim
        anim.SetBool("isDead", true);
        // Disable the enemy
        //this.enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 2f);
        Debug.Log("Died");


    }
}
