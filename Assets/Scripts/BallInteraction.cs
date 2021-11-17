using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteraction : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform ballPos;
    [SerializeField] private float throwForce;
    [SerializeField] private float takeCD;
    private bool hasBall;
    private bool canTakeBall;

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
        }
    }

    public void ThrowBall(Vector3 direction) 
    {
        if (hasBall) 
        { 
            rb.isKinematic = false;
            rb.transform.parent = null;
            rb.AddForce(direction * throwForce);
            hasBall = false;
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
}
