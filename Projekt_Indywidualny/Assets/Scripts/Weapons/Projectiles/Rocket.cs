using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Rocket : Explosive
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private AudioClip explosionSound;
    private float startingLifeTime;

    private Rigidbody rb;
    private bool exploded = false;

    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingLifeTime = lifeTime;
        base.explosionParticles = GetComponent<ParticleSystem>();
        base.audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        Debug.Log(Mathf.Pow(startingLifeTime - lifeTime, 2));
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

    public override IEnumerator Explode()
    {
        explosionParticles.Play();
        base.audioSource.clip = explosionSound;
        base.audioSource.Play();
        DisableVisuals();
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, base.explosionRange);
        List<GameObject> damagedObjects = new List<GameObject>();

        foreach (Collider col in objectsInRange)
        {

            Ihp target = col.GetComponent<Ihp>();
            if (target != null && !damagedObjects.Contains(col.gameObject))
            {
                target.TakeDamage(base.explosionDamage * PlayerStatus.Instance.GetCharacterStatValueOfType(StatType.Damage));
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
