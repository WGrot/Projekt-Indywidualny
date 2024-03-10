using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public List<CharacterStat> stats = new List<CharacterStat>();


    public static PlayerStatus Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one PlayerStatus instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i< stats.Count; i++) {
                Debug.Log(stats[i].value);

            }
        }
    }
}
