using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) 
        {
            scoreManager.AddScore(player);
            CameraShaker.instance.CameraShake();
        }
    }
}
