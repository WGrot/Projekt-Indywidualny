using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool isShaking = false;
    [SerializeField] private Camera fpsCam;
    private float shakeForce;
    private float timer;
    private float startingShakeTime;
    Vector3 startingPos;

    #region Singleton
    public static CameraShake Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one CameraShake instance found!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }
    }
    #endregion


    public void OnEnable()
    {
        WeaponHolder.OnWeaponShootCallback += ShakeCameraByWeapon;
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

    public void ShakeCameraByWeapon(int activeWeaponId)
    {
        isShaking = true;
        timer = Inventory.Instance.GetActiveWeapon().ScreenShakeTime;
        startingShakeTime = timer;
        shakeForce = Inventory.Instance.GetActiveWeapon().ScreenShakeIntesivity;
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
