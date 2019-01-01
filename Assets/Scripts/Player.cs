using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 100;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1f)] float shootSoundVolume = 0.25f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1f)] float deathSoundVolume = 0.75f;


    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserDelay = 0.1f;

    Coroutine fireCoroutine;

    float minX;
    float maxX;
    float minY;
    float maxY;
    // Use this for initialization
    void Start()
    {
        PositionToWorldPoint();
    }

    private void PositionToWorldPoint()
    {
        var camera = Camera.main;

        minX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(ContinouslyFire());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator ContinouslyFire()
    {
        while (true)
        {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

            yield return new WaitForSeconds(laserDelay);
        }
    }

    private void Move()
    {
        var deltax = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newPosx = Mathf.Clamp(transform.position.x + deltax, minX, maxX);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newPosx, newPosY);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damager = collider.GetComponent<Damager>();

        if (damager == null) { return; }

        GetDamage(damager);
    }

    private void GetDamage(Damager damager)
    {
        health -= damager.GetDamage();

        damager.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void Die()
    {
        health = 0;
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
