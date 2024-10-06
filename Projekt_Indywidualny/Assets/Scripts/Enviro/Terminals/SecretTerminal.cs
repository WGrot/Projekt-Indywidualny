using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class SecretTerminal : MonoBehaviour
{
    private string input = "";
    [SerializeField] private TextMeshPro inputField;

    public void AppendToInput(int number)
    {
        input += number.ToString();
        ShowInput();
    }

    public void ClearInput()
    {
        input = "";
        ShowInput();
    }

    public void ShowInput()
    {
        inputField.text = input.Substring(Math.Max(0, input.Length - 5)); ;
    }
    
}
