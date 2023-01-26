using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject explosion;
    [SerializeField] private Target targetTag = Target.Player;
    [SerializeField] private int damage = 15;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag.ToString()))
        {
            if (targetTag == Target.Player && other.TryGetComponent(out Player player))
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                player.Life.TakeDamage(damage);
                Destroy(gameObject);
            }
            if (targetTag == Target.Enemy && other.TryGetComponent(out ShipAI ai))
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
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
