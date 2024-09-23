using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] private GameObject flipPoint;
    [SerializeField] private GameObject lookPoint;

    public GameObject FlipPoint { get => flipPoint; private set => flipPoint = value; }


    public static PlayerReferences Instance { get; private set; }
    public GameObject LookPoint { get => lookPoint; set => lookPoint = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one PlayerReference instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
