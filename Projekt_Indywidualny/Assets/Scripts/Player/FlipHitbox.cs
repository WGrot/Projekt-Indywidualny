using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipHitbox : MonoBehaviour
{
    [SerializeField] private Transform hitbox;
    [SerializeField] private float hitboxLifeTime;
    [SerializeField] float damage;
    [SerializeField] private float cooldown;
    [SerializeField] Vector3 flipHalfExtents;
    [SerializeField] Vector3 parryHalfExtents;
    private float timeSinceLastFlip;
    private AudioSource audioSource;
    private InputActions inputActions;


    public delegate void FlippedAction();
    public static event FlippedAction OnFlippedActionCallback;

    public delegate void SuccessfulParryAction();
    public static event SuccessfulParryAction OnSuccessfulParryActionCallback;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeSinceLastFlip = 0;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player_base.FlipTheEnemy.performed += Flip;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player_base.FlipTheEnemy.performed -= Flip;
    }



    public void Flip(InputAction.CallbackContext context)
    {
        if (timeSinceLastFlip > cooldown)
        {
            timeSinceLastFlip = 0;
            if (OnFlippedActionCallback != null)
            {
                OnFlippedActionCallback();
            }
            audioSource.Play();
            DoTheParryin();
            DoTheFlippin();


        }
        else
        {
            return;
        }
    }

    void Update()
    {
        timeSinceLastFlip += Time.deltaTime;
    }

    void DoTheFlippin()
    {
        Collider[] objectsInRange = Physics.OverlapBox(hitbox.position, flipHalfExtents, hitbox.rotation);
        List<GameObject> damagedObjects = new List<GameObject>();
        foreach (Collider col in objectsInRange)
        {

            Ihp target = col.GetComponent<Ihp>();
            if (target != null && !damagedObjects.Contains(col.gameObject) && !col.gameObject.CompareTag("Player"))
            {
                target.TakeDamage(damage);
                damagedObjects.Add(col.gameObject);
            }
        }
    }

    void DoTheParryin()
    {
        Collider[] objectsInRange = Physics.OverlapBox(hitbox.position, parryHalfExtents, hitbox.rotation);
        bool isParrySuccessful = false;

        foreach (Collider col in objectsInRange)
        {

            I_Parryable target = col.GetComponent<I_Parryable>();
            if (target != null)
            {
                target.Parry();
                isParrySuccessful = true;

            }
        }

        if (isParrySuccessful && OnSuccessfulParryActionCallback != null)
        {

            OnSuccessfulParryActionCallback();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.TRS(hitbox.position, hitbox.rotation, flipHalfExtents * 2);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
