using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public Transform player;
    public float range = 10f; // maximum distance enemy can be from player
    public float speed = 2f;

    private bool isDead = false;
    private bool isFollowingPlayer = true;
    
    private void Update()
    {
        // Calculate distance between enemy and player
        float distance = Vector2.Distance(transform.position, player.position);

        // If player is within range, move towards them without pushing them
        if (distance <= range)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    public void OnDeath()
    {
        isDead = true;
        isFollowingPlayer = false;
    }
}
