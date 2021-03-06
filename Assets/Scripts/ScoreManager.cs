using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float scoreMax;
    [SerializeField] private GameObject menu;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private int countdownTime;

    private float scoreP1;
    private float scoreP2;
    [SerializeField] private TextMeshProUGUI scoreTxtP1;
    [SerializeField] private TextMeshProUGUI scoreTxtP2;
    [SerializeField] private Transform[] objects;
    [SerializeField] private Transform[] positions;

    public void resetScore() 
    {
        this.scoreP1 = 0;
        this.scoreP2 = 0;
    }

    public void AddScore(int player) 
    {
        if (player == 1)
        {
            scoreP1++;
            scoreTxtP1.text = scoreP1.ToString();
        }
        else if (player == 2)
        {
            scoreP2++;
            scoreTxtP2.text = scoreP2.ToString();
        }
        StartCoroutine(Restart(countdownTime));
    }

    private void ResetPosition() 
    {
        objects[2].GetComponent<Rigidbody>().velocity = Vector3.zero;
        objects[2].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        for (int i = 0; i < objects.Length ; i++)
        {
            objects[i].position = positions[i].position;
        }
    }

    private void CheckIfWin() 
    {
        if (scoreP1 >= scoreMax || scoreP2 >= scoreMax) 
        {
            menu.SetActive(true);
            resetScore();
        }

    }

    IEnumerator Restart(float t)
    {
        t -= 1;
        countdownText.text = t.ToString();
        yield return new WaitForSeconds(1);

        if (t > 0)
        {
            StartCoroutine(Restart(t));
        }
        else 
        {
            countdownText.text = "";
            ResetPosition();
        }

        /*if(countdownTime > 0)
        {
            countdownTime--;
            yield return new WaitForSeconds(1);
            countdownText.text = countdownTime.ToString();
            StartCoroutine(Restart());
        }
        else
        {
            countdownTime = 3;
            //StopCoroutine(Restart());
            ResetPosition();
        }*/
  
    }
}
