using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [Header ("Bullet Mangement")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    [Header ("Shooting Stats")]
    public float bulletSpeed = 10.0f;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        var bullet = ObjectPooler.Instance.GetBullet();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletSpawnPoint.up * bulletSpeed;
    }
}
