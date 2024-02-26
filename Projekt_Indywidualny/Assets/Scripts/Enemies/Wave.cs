using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] GameObject[] enemies;

    public void StartWave()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    public bool IsWaveDefeated()
    {
        bool result = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                result = false;
            }
        }

        return result;
    }


}
