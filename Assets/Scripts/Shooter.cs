using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab1;
    [SerializeField] private GameObject projectilePrefab2;
    [SerializeField] private GameObject projectilePrefab3;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private float baseFiringRate = 0.2f;
    [SerializeField] private float firingRateVariance = 0;
    [SerializeField] private float minimumFiringRate = 0.1f;
    [SerializeField] private bool useAI;
    [SerializeField] private float canontime1 = 6f;
    [SerializeField] private float canontime2 = 12f;
    
    private float time = 0f;
    
    [HideInInspector]
    public bool isFiring;

    private Coroutine firingCor;
    private Vector2 moveDirection;
    private void Start()
    {
       // projectilePrefab1.SetActive(true);
      //  projectilePrefab2.SetActive(false);
       // projectilePrefab3.SetActive(false);

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
        time += Time.deltaTime;
        Debug.Log("Czas: " + time);

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
            if (time <= canontime1)
            {
                GameObject projectile = Instantiate(projectilePrefab1, transform.position, Quaternion.identity);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = moveDirection * projectileSpeed;
                }
                Destroy(projectile, projectileLifeTime);

            }
            else if (time > canontime1 && time <= canontime2)
            {
                GameObject projectile = Instantiate(projectilePrefab2, transform.position, Quaternion.identity);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = moveDirection * projectileSpeed;
                }
                Destroy(projectile, projectileLifeTime);
            }
            else 
            {
                GameObject projectile = Instantiate(projectilePrefab3, transform.position, Quaternion.identity);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = moveDirection * projectileSpeed;
                }
                Destroy(projectile, projectileLifeTime);
            }
           


            float timeToNextProjectile =
                Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
