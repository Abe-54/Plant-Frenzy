using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score;
    public TMP_Text scoreTxt;
    public float scoreMultiplier = 1f;
    private float minMultiplier = 1f;
    private float maxMultiplier = 5f;
    public float accelerationTime = 70f;
    private float time;

    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        minMultiplier = scoreMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "" + Mathf.Round(score);
        scoreMultiplier = Mathf.SmoothStep(minMultiplier, maxMultiplier, time / accelerationTime );
        time += Time.deltaTime;
    }

    public IEnumerator StartScore()
    {
        while (!player.isDead)
        {
            score += 1;
            yield return new WaitForSeconds(1f / scoreMultiplier);
        }
    }
}
