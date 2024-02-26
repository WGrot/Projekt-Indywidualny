using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    int activeWave = 0;

    public UnityEvent OnAllWavesDefeated;
    public void OnEnable()
    {
        EnemyHpBase.OnEnemyDeath += StartNextWave;
    }

    public void OnDisable()
    {
        EnemyHpBase.OnEnemyDeath -= StartNextWave;
    }
    public void StartFirstWave()
    {
        waves[0].StartWave();
    }

    public void StartNextWave()
    {
        Debug.Log("attempted to start next wave");
        if (!waves[activeWave].IsWaveDefeated())
        {
            return;
        }

        if (activeWave == waves.Length -1)
        {
            OnAllWavesDefeated.Invoke();
            gameObject.SetActive(false);
            return;
        }
        activeWave++;
        waves[activeWave].StartWave();
    }
}
