using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    [SerializeField] GameObject GameManager;
    [SerializeField] bool shouldDestroyOldManager = false;

    public void Awake()
    {
        GameObject oldManager = GameObject.FindGameObjectWithTag("GameManager");

		if (shouldDestroyOldManager)
		{
            Destroy(oldManager);
		}

		if (oldManager == null)
        {
            Instantiate(GameManager);
        }
        Destroy(gameObject);
    }
}
