using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TMP_Text distanceTxt;
    public TMP_Text gameOverTag;
    public String tag;
    
    public GameObject mainMenuScreen;
    public GameObject inGameUIScreen;
    public GameObject seed;

    public List<String> gameOverTags;

    [SerializeField] private PlayerController player;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private SpawnManager rockSpawnManager;
    [SerializeField] private SpawnManager waterSpawnManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        
        player.gameObject.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        StartCoroutine(ShowDeathScreen());
    }

    public void GenerateRandomTag()
    {
        int tagToChoose = Random.Range(0, gameOverTags.Count);
        tag = gameOverTags[tagToChoose];
    }

    public IEnumerator ShowDeathScreen()
    {
        gameOverScreen.SetActive(true);
        GenerateRandomTag();
        gameOverTag.text = tag;
        distanceTxt.text = "Distance: " + Mathf.Round(scoreManager.score);
        yield return null;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Play()
    {
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        mainMenuScreen.SetActive(false);
        seed.SetActive(true);
        yield return new WaitUntil(() => seed.GetComponent<SeedOpening>().GetSeedOpened());
        inGameUIScreen.SetActive(true);
        player.gameObject.SetActive(true);
        rockSpawnManager.InvokeSpawning();
        waterSpawnManager.InvokeSpawning();
        StartCoroutine(player.ReduceHealth());
        StartCoroutine(scoreManager.StartScore());
    }
}
