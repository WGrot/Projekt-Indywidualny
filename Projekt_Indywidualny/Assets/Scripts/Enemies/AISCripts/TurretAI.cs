using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    [SerializeField] float timeBetweenRotation;
    [SerializeField] float stopTime;
    [SerializeField] GameObject RotatingPart;


    void Start()
    {
        StartCoroutine(RotateOverTime(90f, timeBetweenRotation));
    }

    IEnumerator RotateOverTime(float angle, float time)
    {
        while (true)
        {
            Quaternion startRotation = RotatingPart.transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);

            for (float t = 0; t < time; t += Time.deltaTime)
            {
                RotatingPart.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / time);
                yield return null;
            }

            RotatingPart.transform.rotation = endRotation;
            yield return new WaitForSeconds(stopTime);
        }

    }
}

