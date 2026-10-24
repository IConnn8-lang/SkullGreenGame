using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform Player;
    public Transform muzzle;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletSnipe;
    public GameObject bulletHeavy;
    public Animator animator;
    public int shooterCounter;

    public float bulletForce = 30f;
    public float FireRate = 2f;
    private float lastfired = 0f;

    Vector3 doubleShotOffset = new Vector3(0, 0, 0);
    public float bulletDistance = 0.5f;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("Shooting", true);
            if ((Time.time - lastfired) > (1 / FireRate))
            {
                lastfired = Time.time;
                Shoot();
            }
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }

    void Shoot()
    {
        // 1. หาตำแหน่งเมาส์ในโลกของเกม (World Space)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // บังคับให้แกน Z เป็น 0 สำหรับเกม 2D

        // 2. คำนวณทิศทางจากจุดยิงไปยังเมาส์
        Vector3 direction = (mousePosition - firePoint.position).normalized;

        // 3. คำนวณมุม (Angle) เพื่อให้ Sprite ของกระสุนหันหน้าไปทางที่ยิง
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);

        // 4. คำนวณระยะเยื้อง (Offset) สำหรับกระสุนคู่ โดยใช้เวกเตอร์ตั้งฉาก (Perpendicular Vector)
        doubleShotOffset = new Vector3(-direction.y, direction.x, 0f) * bulletDistance;

        if (shooterCounter == 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }

        if (shooterCounter == 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + doubleShotOffset, bulletRotation);
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position - doubleShotOffset, bulletRotation);

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
            bullet1.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }

        if (shooterCounter == 2)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + (2 * doubleShotOffset), bulletRotation);
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position - (2 * doubleShotOffset), bulletRotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
            bullet1.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
            bullet2.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }

        if (shooterCounter == 3)
        {
            GameObject heavy = Instantiate(bulletHeavy, firePoint.position, bulletRotation);
            // กระสุนหลัง ให้หันกลับด้าน (หมุนเพิ่ม 180 องศา)
            Quaternion backwardRotation = Quaternion.Euler(0f, 0f, angle + 180f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, backwardRotation);

            heavy.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
            // ยิงไปข้างหลัง
            bullet.GetComponent<Rigidbody2D>().AddForce(-direction * bulletForce, ForceMode2D.Impulse);
        }

        if (shooterCounter == 4)
        {
            GameObject heavy = Instantiate(bulletHeavy, firePoint.position + doubleShotOffset, bulletRotation);
            GameObject heavy1 = Instantiate(bulletHeavy, firePoint.position - doubleShotOffset, bulletRotation);
            GameObject heavy2 = Instantiate(bulletHeavy, firePoint.position, bulletRotation);

            Vector3 rightForce = new Vector3(direction.y, -direction.x, 0f);
            Vector3 leftForce = new Vector3(-direction.y, direction.x, 0f);

            heavy.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
            heavy1.GetComponent<Rigidbody2D>().AddForce(rightForce * bulletForce, ForceMode2D.Impulse);
            heavy2.GetComponent<Rigidbody2D>().AddForce(leftForce * bulletForce, ForceMode2D.Impulse);
        }

        if (shooterCounter == 5)
        {
            GameObject snipe = Instantiate(bulletSnipe, firePoint.position, bulletRotation);
            snipe.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }
    }

    public void setFireRate(float newRate)
    {
        FireRate = newRate;
    }

    public void setBulletForce(float newBF)
    {
        bulletForce = newBF;
    }

    public void setShooterCounter(int newSC)
    {
        shooterCounter = newSC;
    }
}