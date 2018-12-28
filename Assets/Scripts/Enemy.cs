using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectTile;
    [SerializeField] float projectTileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplotion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1f)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1f)] float shootSoundVolume = 0.25f;

    // Use this for initialization
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CounDownAndShoot();
    }

    private void CounDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Fire();

            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        var laser = Instantiate(projectTile, transform.position, Quaternion.identity);

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectTileSpeed);

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damager = other.gameObject.GetComponent<Damager>();

        if (damager == null) { return; }

        ProcessHit(damager);
    }

    private void ProcessHit(Damager damager)
    {
        health -= damager.GetDamage();

        damager.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        var vfx = Instantiate(deathVFX, transform.position, transform.rotation);

        Destroy(vfx, durationOfExplotion);

        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
