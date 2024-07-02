using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float CheckRange;

    public bool IsGrounded { get => isGrounded; private set => isGrounded = value; }
    private bool isGrounded = false;

    private void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.down, CheckRange, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
