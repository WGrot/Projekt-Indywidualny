using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnEnable()
    {
        LevelGenerationV2.OnLevelGenerated += SpawnPlayer;
    }

    private void OnDisable()
    {
        LevelGenerationV2.OnLevelGenerated -= SpawnPlayer;
    }


    void SpawnPlayer()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
