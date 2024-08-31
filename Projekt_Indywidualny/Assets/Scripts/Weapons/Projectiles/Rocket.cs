using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] protected float explosionRange;
    protected ParticleSystem explosionParticles;
    protected AudioSource audioSource;


    private float startingLifeTime;

    private bool exploded = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingLifeTime = lifeTime;
        explosionParticles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        rb.velocity = transform.forward * Mathf.Clamp(Mathf.Pow(startingLifeTime - lifeTime,2), 0, speed) * speed;
        if (lifeTime < 0 && !exploded)
        {
            exploded = true;
            StartCoroutine(Explode());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!exploded)
        {
            exploded = true;
            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        explosionParticles.Play();
        audioSource.clip = explosionSound;
        audioSource.Play();
        DisableVisuals();
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRange);
        List<GameObject> damagedObjects = new List<GameObject>();

        foreach (Collider col in objectsInRange)
        {

            Ihp target = col.GetComponent<Ihp>();
            if (target != null && !damagedObjects.Contains(col.gameObject))
            {
                target.TakeDamage(damage);
                damagedObjects.Add(col.gameObject);
            }
        }
        yield return new WaitForSeconds(explosionSound.length);

        Destroy(gameObject);

    }

    private void DisableVisuals()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
        GetComponent<TrailRenderer>().enabled = false;
    }

}
