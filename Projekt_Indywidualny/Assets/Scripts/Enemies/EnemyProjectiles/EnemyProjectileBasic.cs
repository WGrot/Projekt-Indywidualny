using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBasic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    private SpriteRenderer spriteRenderer;
    //private Rigidbody rb;
    private TrailRenderer trailRenderer;

    public delegate void OnDisableCallback(EnemyProjectileBasic instance);
    public event OnDisableCallback OnDisableAction;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    /*
    private void OnEnable()
    {
        Invoke("ActivateVisuals", 0.05f);
    }
    */
    private void ActivateVisuals()
    {
        trailRenderer.enabled = true;
        spriteRenderer.enabled = true;

    }

    public void DisableVisuals()
    {
        trailRenderer.enabled = false;
        spriteRenderer.enabled = false;

    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void Shoot(Vector3 position, Vector3 direction, float newSpeed, float newLifeTime, float newDamage)
    {
        transform.forward = direction;
        transform.position = position;

        

        trailRenderer.Clear();
        speed = newSpeed;
        lifeTime = newLifeTime;
        damage = newDamage;

        //rb.velocity = transform.forward * speed;
        Invoke("ActivateVisuals", 0.05f);
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    public void SetLifeTime(int newLifeTime)
    {
        lifeTime = newLifeTime;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void Update()
    {
        transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            OnDisableAction?.Invoke(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.Instance.TakeDamage(damage);
            OnDisableAction?.Invoke(this);
        }
        else
        {
            OnDisableAction?.Invoke(this);
        }
    }

}
