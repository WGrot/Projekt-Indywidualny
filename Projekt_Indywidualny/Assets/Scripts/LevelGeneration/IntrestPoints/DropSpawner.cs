using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthDrop;
    [SerializeField] private GameObject ammoDrop;
    [SerializeField] private int chanceForHealthDrop;
    [SerializeField] private int chanceForAmmoDrop;


    public void DropStuff()
    {
        int rand = Random.Range(0, 100);
        if(rand < chanceForHealthDrop)
        {
            Instantiate(healthDrop, transform.position, Quaternion.identity);
        }

        rand = Random.Range(0, 100);
        if (rand < chanceForAmmoDrop)
        {
            Instantiate(ammoDrop, transform.position - new Vector3(-2, 0, 0), Quaternion.identity);
        }

    }
}
