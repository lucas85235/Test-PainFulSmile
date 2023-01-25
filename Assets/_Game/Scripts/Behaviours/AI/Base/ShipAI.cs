using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
    [SerializeField] protected float followSpeed = 2f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected Life life;

    public Life Life { get => life; }
    protected GameObject _player;

    protected virtual void Start()
    {
        _player = GameObject.FindWithTag("Player");
        life.OnDeath.AddListener(OnDeath);
    }

    protected void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, followSpeed * Time.deltaTime);
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
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        GameManager.Instance.AddScore();
    }
}
