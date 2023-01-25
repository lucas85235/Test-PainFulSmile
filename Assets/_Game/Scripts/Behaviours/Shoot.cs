using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    public void ShootBullet(Transform spaw = null)
    {
        if (spaw == null) spaw = bulletSpawn;
        var bullet = Instantiate(bulletPrefab, spaw.position, spaw.rotation);
        bullet.AddForce(bullet.transform.right * 500f);
        Destroy(bullet.gameObject, 2f);
    }
}
