using UnityEngine;

public class Shooter : ShipAI
{
    [SerializeField] private float followDistance = 10f;
    [SerializeField] private float shootingDistance = 10f;
    [SerializeField] private float shootingInterval = 2f;

    private Shoot _shoot;
    private float _nextShot;

    protected override void Start()
    {
        base.Start();
        _shoot = GetComponent<Shoot>();
        _nextShot = Time.time + shootingInterval;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > followDistance)
        {
            FollowPlayer();
        }
        if (distance <= shootingDistance)
        {
            LookAtPlayer();
            if (Time.time > _nextShot)
            {
                _shoot.ShootBullet();
                _nextShot = Time.time + shootingInterval;
            }
        }
    }
}
