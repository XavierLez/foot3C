using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteraction : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform ballPos;
    [SerializeField] private SphereCollider trigger;
    [SerializeField] private SphereCollider collider;
    [SerializeField] private float throwForce;
    [SerializeField] private float takeCD;
    private bool hasBall;
    private bool canTakeBall;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CameraShaker cameraShaker;

    public GameObject ball;

    private void Start()
    {
        canTakeBall = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ball") && canTakeBall) 
        {
            rb.transform.parent = ballPos;
            rb.transform.position = ballPos.position;
            rb.isKinematic = true;
            hasBall = true;
            canTakeBall = false;
            ball = other.gameObject;
            collider.enabled = false;
            trigger.enabled = false;
        }
    }

    private BallInteraction otherBI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && playerInput.isDashing) 
        {
            otherBI = collision.transform.GetComponent<BallInteraction>();
            if (otherBI.GetHasBall()) 
            {
                TakeBall(otherBI.LeaveBall());
                cameraShaker.CameraShake();
            }
        }
    }

    public void TakeBall(GameObject ball) 
    {
        this.ball = ball;
        rb.transform.parent = ballPos;
        rb.transform.position = ballPos.position;
        rb.isKinematic = true;
        hasBall = true;
        canTakeBall = false;
    }

    public GameObject LeaveBall() 
    {
        if (hasBall)
        {
            rb.isKinematic = false;
            rb.transform.parent = null;
            hasBall = false;
            GameObject ball = this.ball;
            this.ball = null;
            StartCoroutine(CanTakeBallCoroutine(takeCD));
            return ball;
        }
        else return null;
    }

    public void ThrowBall(Vector3 direction) 
    {
        if (hasBall) 
        { 
            rb.isKinematic = false;
            rb.transform.parent = null;
            rb.AddForce(direction * throwForce);
            hasBall = false;
            ball = null;
            collider.enabled = true;
            trigger.enabled = true;
            StartCoroutine(CanTakeBallCoroutine(takeCD));
        }
    }

    IEnumerator CanTakeBallCoroutine(float t) 
    {
        yield return new WaitForSeconds(t);

        canTakeBall = true;
    }

    public float GetThrowForce() 
    {
        return this.throwForce;
    }
    public bool GetHasBall() 
    {
        return this.hasBall;
    }
}
