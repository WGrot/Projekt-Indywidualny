using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SecretTerminal : MonoBehaviour
{
    private string input = "";
    [SerializeField] private TextMeshPro inputField;
    [SerializeField] private Transform SecretSpawnPoint;
    [SerializeField] private List<GameObject> secrets;
    private Dictionary<string, GameObject> secretDictionary;
    GameObject activeSecret;

    public UnityEvent onCorrectCode;
    public UnityEvent onWrongCode;


    private void Start()
    {
        secretDictionary= new Dictionary<string, GameObject>();
        LoadSecretsToDictionary();
    }

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

    public void VerifyCode()
    {
        GameObject chosenSecret;
        Destroy(activeSecret);
        if (secretDictionary.TryGetValue(input, out chosenSecret))
        {
            ClearInput();
            inputField.text = "OK";
            activeSecret = Instantiate(chosenSecret, SecretSpawnPoint.position, SecretSpawnPoint.rotation);
            if (onCorrectCode!= null)
            {
                onCorrectCode.Invoke();
            }
        }
        else
        {
            ClearInput();
            inputField.text = "ERROR";
            if (onWrongCode != null)
            {
                onWrongCode.Invoke();
            }
        }
    }

    public void ShowInput()
    {
        inputField.text = input.Substring(Math.Max(0, input.Length - 5)); ;
    }
    private void LoadSecretsToDictionary()
    {
        foreach (GameObject secret in secrets)
        {
            Secret secretScript = secret.GetComponent<Secret>();
            string code = secretScript.GetCode();
            secretDictionary.Add(code, secret);
        }
    }

}
