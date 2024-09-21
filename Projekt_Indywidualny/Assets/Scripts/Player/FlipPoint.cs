using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipPoint : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] Vector3 halfExtents;
    private void OnEnable()
    {
        Collider[] objectsInRange = Physics.OverlapBox(transform.position, halfExtents,transform.rotation);
        List<GameObject> damagedObjects = new List<GameObject>();
        foreach (Collider col in objectsInRange)
        {

            Ihp target = col.GetComponent<Ihp>();
            if (target != null && !damagedObjects.Contains(col.gameObject) && !col.gameObject.CompareTag("Player"))
            {
                target.TakeDamage(damage);
                damagedObjects.Add(col.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, halfExtents * 2);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }


}
