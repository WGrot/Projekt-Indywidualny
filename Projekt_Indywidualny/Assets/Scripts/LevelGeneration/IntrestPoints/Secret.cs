using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{
    [SerializeField] private string Code;

    public string GetCode()
    {
        return Code;
    }
}
