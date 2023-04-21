using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private float baseFiringRate = 0.2f;
    [SerializeField] private float firingRateVariance = 0;
    [SerializeField] private float minimumFiringRate = 0.1f;
    [SerializeField] private bool useAI;
    
    //[HideInInspector]
    public bool isFiring;
    public bool isFiring2Rocket;
    public bool isFiring3Rocket;

    private Coroutine firingCor;
    private Vector2 moveDirection;
    private void Start()
    {
        if (useAI)
        {
            isFiring = true;
            moveDirection = transform.up * -1;
        }
        else
        {
            moveDirection = transform.up;
        }
    }

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCor == null)
        {
            firingCor = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCor != null)
        {
            StopCoroutine(firingCor);
            firingCor = null;
        }
        
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (isFiring2Rocket == true)
            {
                GameObject projectile1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                GameObject projectile2 = Instantiate(projectilePrefab, transform.position + new Vector3(-0.5f, 1), Quaternion.identity);

                Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
                if (rb1 != null && rb2 != null)
                {
                    rb1.velocity = moveDirection * projectileSpeed;
                    rb2.velocity = moveDirection * projectileSpeed;
                }

                Destroy(projectile1, projectileLifeTime);
                Destroy(projectile2, projectileLifeTime);
            }
            if (isFiring3Rocket == true)
            {
                GameObject projectile1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                GameObject projectile2 = Instantiate(projectilePrefab, transform.position + new Vector3(-0.5f, 1), Quaternion.identity);
                GameObject projectile3 = Instantiate(projectilePrefab, transform.position + new Vector3(0.5f, 1), Quaternion.identity);

                Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
                Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();
                if (rb1 != null && rb2 != null && rb3 != null)
                {
                    rb1.velocity = moveDirection * projectileSpeed;
                    rb2.velocity = moveDirection * projectileSpeed;
                    rb3.velocity = moveDirection * projectileSpeed;
                }

                Destroy(projectile1, projectileLifeTime);
                Destroy(projectile2, projectileLifeTime);
                Destroy(projectile3, projectileLifeTime);
            }

            //Deafult shooting with 1 Rocket
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = moveDirection * projectileSpeed;
            }

            Destroy(projectile, projectileLifeTime);

            float timeToNextProjectile =
                Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToNextProjectile);
        }  
    }
}
