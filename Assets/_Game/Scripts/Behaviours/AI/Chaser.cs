using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : ShipAI
{
    private void Update()
    {
        FollowPlayer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.TryGetComponent(out Player player))
        {
            player.Life.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
