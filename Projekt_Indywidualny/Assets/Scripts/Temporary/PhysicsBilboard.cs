using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bilboard Script dla obiektów z rigidbody, dzieki niemu dzia³a na nich interpolacja

[RequireComponent(typeof(Rigidbody))]
public class PhysicsBilboard : MonoBehaviour
{


    Camera mainCamera;
    bool isCameraFound = false;
    Rigidbody rb;


    private void OnEnable()
    {
        //Przypisujê szukanie kamery do eventu po stworzeniu lochu
        //To dla obiektów które zosta³y stworzone przed spawnem gracza
        LevelGenerationV2.OnLevelGenerated += FindCamera;
        rb = GetComponent<Rigidbody>();
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

    void Update()
    {
        if (!isCameraFound)
        {
            return;
        }
        Quaternion newRotation = mainCamera.transform.rotation;
        newRotation.Set(0, newRotation.y, 0, newRotation.w);
        newRotation.Normalize();
        rb.MoveRotation(newRotation);
    }
}
