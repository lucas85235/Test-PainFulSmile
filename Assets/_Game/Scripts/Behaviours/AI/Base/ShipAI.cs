using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipAI : MonoBehaviour
{
    [SerializeField] protected float followSpeed = 2f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected Life life;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected GameObject explosion;
    [SerializeField] private SpriteRenderer hullSprite;
    [SerializeField] private Sprite[] damgeShipSprites;

    private int currentsHullSprite = 0;

    public Life Life { get => life; }
    protected GameObject _player;

    protected virtual void Start()
    {
        _player = GameObject.FindWithTag("Player");

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = followSpeed;

        life.OnDeath.AddListener(OnDeath);
        life.OnTakeDamage.AddListener(OnTakeDamage);
    }

    protected void FollowPlayer()
    {
        // transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, followSpeed * Time.deltaTime);
        if (_player == null) return;
        agent.SetDestination(_player.transform.position);
        LookAtPlayer();
    }

    protected void LookAtPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSpeed);
    }

    private void OnDeath()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTakeDamage()
    {
        var i = Mathf.Clamp(currentsHullSprite, 0, damgeShipSprites.Length);
        hullSprite.sprite = damgeShipSprites[i];
        currentsHullSprite++;
    }

    protected virtual void OnDestroy()
    {
        GameManager.Instance.AddScore();
    }
}
