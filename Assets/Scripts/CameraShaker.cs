using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;
    [SerializeField] public bool shakingStarted;
    private Vector3 originPosition;
    public static CameraShaker instance;
    [SerializeField] private Transform centerOfTheScene;

    private void Start()
    {
        instance = this;
        
    }

    public void CameraShake()
    {
        if (shakingStarted == false)
        {
            originPosition = transform.position;
            StartCoroutine(CameraShakeStopper());
        }
  
        transform.position = originPosition + Random.insideUnitSphere * shakeAmount;
    }

    private void FixedUpdate()
    {
        if (shakingStarted)
        {
            CameraShake();
        }
        
    }

    private IEnumerator CameraShakeStopper()
    {
        shakingStarted = true;
        yield return new WaitForSeconds(shakeDuration);
        transform.position = centerOfTheScene.position;
        shakingStarted = false;
        
    }
}
