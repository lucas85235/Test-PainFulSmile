using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private List<Transform> bulletSideSpawns;

    private Shoot _shoot;

    private void Awake()
    {
        _shoot = GetComponent<Shoot>();
    }

    private void Update()
    {
        MoveAndRotate();

        if (Input.GetMouseButtonDown(0)) // || Input.GetKeyDown(KeyCode.Space))
        {
            _shoot.ShootBullet();
        }
        if (Input.GetMouseButtonDown(1)) // || Input.GetKeyDown(KeyCode.Space))
        {
            SideShoot();
        }
    }

    private void MoveAndRotate()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += (Vector3)movement.normalized * speed * Time.deltaTime;
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SideShoot()
    {
        foreach (var item in bulletSideSpawns)
        {
            _shoot.ShootBullet(item);
        }
    }
}
