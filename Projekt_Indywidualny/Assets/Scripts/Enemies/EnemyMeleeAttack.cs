using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player was attacked");
        if (other.CompareTag("Player"))
        {
            PlayerStatus.Instance.TakeDamage(damage);
        }
    }
}
