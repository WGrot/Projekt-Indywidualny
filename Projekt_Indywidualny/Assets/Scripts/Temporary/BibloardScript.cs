using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibloardScript : MonoBehaviour
{
    Camera mainCamera;
    bool isCameraFound = false;


    private void OnEnable()
    {
        LevelGenerationV2.OnLevelGenerated += FindCamera;
    }

    private void OnDisable()
    {
        LevelGenerationV2.OnLevelGenerated -= FindCamera;
    }
    void FindCamera()
    {
        try
        {
            mainCamera = Camera.main;
        }catch(NullReferenceException e)
        {
            return;
        }
        isCameraFound= true;
    }

    void LateUpdate()
    {
        if (!isCameraFound)
        {
            return;
        }
        Vector3 newRotation = mainCamera.transform.eulerAngles;

        newRotation.x = 0;
        newRotation.z = 0;

        transform.eulerAngles = newRotation;
    }
}
