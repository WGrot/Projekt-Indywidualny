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
        //Przypisujê szukanie kamery do eventu po stworzeniu lochu
        //To dla obiektów które zosta³y stworzone przed spawnem gracza
        LevelGenerationV2.OnLevelGenerated += FindCamera; 
        FindCamera();
    }

    private void OnDisable()
    {
        LevelGenerationV2.OnLevelGenerated -= FindCamera;
        FindCamera();
    }
    void FindCamera()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
            isCameraFound = true;
        }

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
