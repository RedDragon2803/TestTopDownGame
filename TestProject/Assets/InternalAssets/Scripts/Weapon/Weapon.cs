using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject Bullet;
    public WeaponStats stats;
    public BoolVariable ShootInput;
    public BoolVariable isTurnedRight;
    private int currentAmmo = -1;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        if (currentAmmo == -1)
            currentAmmo = stats.clipSize;
    }

    void OnEnable()
    {
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (ShootInput.Value && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / stats.fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(stats.reloadTime);

        currentAmmo = stats.clipSize;
        isReloading = false;
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(Bullet, ShootPoint.position, isTurnedRight.Value ? ShootPoint.rotation : ShootPoint.rotation * Quaternion.Euler(0, 180f, 0));
        newBullet.GetComponent<Bullet>().StartFly(stats.bulletSpeed, stats.damage);
        currentAmmo--;
    }
}
