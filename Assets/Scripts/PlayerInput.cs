using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private string inputHorizontal;
    [SerializeField] private string inputForward;
    [SerializeField] private string inputDash;
    [SerializeField] private string inputThrow;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCD;
    private bool isOnCD;
    private bool canMove;
    public bool isDashing;

    [SerializeField] private BallInteraction ballInteraction;

    private float xDirection;
    private float zDirection;

    private float inputAxisX;
    private float inputAxisZ;

    private void Start()
    {
        this.canMove = true;
    }

    private void Update()
    {
        inputAxisX = Input.GetAxisRaw(inputHorizontal);
        inputAxisZ = Input.GetAxisRaw(inputForward);

        if (canMove) rb.velocity = new Vector3(inputAxisX, rb.velocity.y, inputAxisZ).normalized * speed;

        Direction();

        if (Input.GetButton(inputDash) && !isOnCD)
        {
            Dash();
        }

        if (Input.GetButton(inputThrow)) 
        {
            ballInteraction.ThrowBall(new Vector3(xDirection, 1, zDirection));
        }


        LookAt();
    }

    private void Dash()
    {
        StartCoroutine(DashCoroutine(dashTime));
        StartCoroutine(CDCoroutine(dashCD));
    }

    private void Direction()
    {
        if (inputAxisX != 0 || inputAxisZ != 0)
        {
            xDirection = inputAxisX;
            zDirection = inputAxisZ;
        }
    }

    IEnumerator DashCoroutine(float t) 
    {
        isDashing = true;
        canMove = false;
        isOnCD = true;
        trail.emitting = true;
        rb.AddForce(Vector3.right * xDirection * speedMultiplier + Vector3.forward * zDirection * speedMultiplier, ForceMode.VelocityChange);

        yield return new WaitForSeconds(t);

        trail.emitting = false;
        rb.velocity = Vector3.zero;
        canMove = true;
        isDashing = false;
    }

    IEnumerator CDCoroutine(float t) 
    {
        yield return new WaitForSeconds(t);

        isOnCD = false;
    }

    private void LookAt() 
    {
        transform.LookAt(transform.position + new Vector3(xDirection, 0f, zDirection));
    }

    public void SetCanMove(bool canMove) 
    {
        this.canMove = canMove;
    }
}
