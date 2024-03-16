using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    public void Awake()
    {
        if (GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            Instantiate(GameManager);
        }
        Destroy(gameObject);
    }
}
