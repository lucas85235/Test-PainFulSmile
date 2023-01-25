using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float followDistance = 10f;
    public float shootingDistance = 10f;
    public float shootingInterval = 2f;
    public float followSpeed = 2f;

    private Shoot _shoot;
    private GameObject _player;
    private float _nextShot;

    private void Start()
    {
        _shoot = GetComponent<Shoot>();
        _player = GameObject.FindWithTag("Player");
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

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, followSpeed * Time.deltaTime);
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSpeed);
    }
}
