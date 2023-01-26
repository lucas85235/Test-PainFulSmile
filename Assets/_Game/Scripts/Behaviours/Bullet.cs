using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Target targetTag = Target.Player;
    [SerializeField] private int damage = 15;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag.ToString()))
        {
            if (targetTag == Target.Player && other.TryGetComponent(out Player player))
            {
                player.Life.TakeDamage(damage);
                Destroy(gameObject);
            }
            if (targetTag == Target.Enemy && other.TryGetComponent(out ShipAI ai))
            {
                ai.Life.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private enum Target
    {
        Player,
        Enemy
    }
}
