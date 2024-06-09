using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDepth : MonoBehaviour
{
    #region Singleton
    public static DungeonDepth Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one DungeonDepth instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    [SerializeField] private int difficultyIncrease = 1;
    private int dungeonDepth = -1;
    private float difficultyModifier = 1;

    public float DifficultyModifier { get => difficultyModifier; private set => difficultyModifier = value; }

    private void OnEnable()
    {
        LevelGenerationV2.OnLevelGenerated += IncreaseDungeonDepth;
    }

    public void OnDisable()
    {
        LevelGenerationV2.OnLevelGenerated -= IncreaseDungeonDepth;
    }
    private void IncreaseDungeonDepth()
    {
        dungeonDepth++;
        difficultyModifier = 1 + (float)(dungeonDepth * difficultyIncrease) / 10;
    }
}
