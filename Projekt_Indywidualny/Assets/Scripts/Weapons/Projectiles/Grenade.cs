using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : Explosive
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private Rigidbody rb;
    private bool exploded = false;

    
    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity += transform.forward * speed;
        base.explosionParticles = GetComponent<ParticleSystem>();
        base.audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && !exploded)
        {
            exploded = true;
            StartCoroutine("Explode");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.TryGetComponent<Ihp>(out var enemyHp) && collision.gameObject.CompareTag("Enemy") && !exploded)
        {
            exploded = true;
            StartCoroutine("Explode");
        }
    }

    public override IEnumerator Explode()
    {
        explosionParticles.Play();
        audioSource.Play();
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
        yield return new WaitForSeconds(audioSource.clip.length);
        
    }

    private void DisableVisuals()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
    }
    
}
