using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : Projectile
{
    private bool exploded = false;
    protected ParticleSystem explosionParticles;
    protected AudioSource audioSource;
    [SerializeField] protected float explosionRange;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity += transform.forward * speed;
        explosionParticles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && !exploded)
        {
            exploded = true;
            StartCoroutine(Explode());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.TryGetComponent<Ihp>(out var enemyHp) && collision.gameObject.CompareTag("Enemy") && !exploded)
        {
            exploded = true;
            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        explosionParticles.Play();
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
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }

    private void DisableVisuals()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
    }
    
}
