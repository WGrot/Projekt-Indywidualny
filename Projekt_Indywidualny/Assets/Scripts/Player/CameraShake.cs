using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool isShaking = false;
    [SerializeField] private Camera fpsCam;
    private float shakeForce;
    private float timer;
    private float startingShakeTime;
    Vector3 startingPos;


    public void OnEnable()
    {
        WeaponHolder.OnWeaponShootCallback += ShakeCameraDefaultValues;
    }


    void Start()
    {
        startingPos = transform.localPosition;
    }

    public void ShakeCamera(float intensity, float time)
    {
        isShaking = true;
        timer = time;
        startingShakeTime = time;
        shakeForce = intensity;
    }

    public void ShakeCameraDefaultValues(int activeWeaponId)
    {
        isShaking = true;
        timer = 0.1f;
        startingShakeTime = 0.1f;
        shakeForce = 0.05f;
    }

    void FixedUpdate()
    {

        if (isShaking == true && timer > 0)
        {
            fpsCam.transform.localPosition = startingPos + Random.insideUnitSphere * shakeForce * timer / startingShakeTime;
            timer -= Time.deltaTime;
        }
        else
        {
            isShaking = false;
            fpsCam.transform.localPosition = startingPos;
        }

    }

}
