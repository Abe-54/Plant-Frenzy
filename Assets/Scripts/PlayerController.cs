using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float accelerationTime = 20f;
    public float rotationSpeed = 300f;
    public bool canPlayerMove = true;
    private float maxSpeed = 7f;
    private float minSpeed;
    private float time;

    [Header("Bounderies")] 
    public float maxZRot = 90f;
    public float minZRot = -90f;
    public float xMinBoundery;
    public float xMaxBoundery;

    [Header("Health")] public bool isDead = false;

    [Header("UI")] public TMP_Text moistureText;
    
    [Header("Audio")] 
    public AudioSource playerAudioSource;
    public AudioClip hitWater;
    public AudioClip hitRock;
    
    [Header("Collisions")]
    public float speedReducedBy = 0.15f;
    public int waterToReduce = 5;

    private Health healthComponent;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        healthComponent = GetComponent<Health>();

        minSpeed = speed; 
        time = 0 ;
    }

    private void Update()
    {
        moistureText.text = "Water: " + healthComponent.health;
        
        if (healthComponent.health <= 0) isDead = true;

        if (isDead && !gameManager.gameOverScreen.activeInHierarchy)
        {
            canPlayerMove = false;
            gameManager.PlayerDied();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), 1f);
        
        if (canPlayerMove)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            
            if (input.x != 0)
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * input.x);
            }


            if (transform.position.x > xMaxBoundery)
            {
                transform.position = new Vector3(xMaxBoundery, transform.position.y, transform.position.z);
            } else if (transform.position.x < xMinBoundery)
            {
                transform.position = new Vector3(xMinBoundery, transform.position.y, transform.position.z);
            }
            
            speed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime );
            time += Time.deltaTime;
        }
        
        Vector3 playerEulerAngles = transform.rotation.eulerAngles;
        
        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, minZRot, maxZRot);
         
        transform.rotation = Quaternion.Euler(playerEulerAngles);
    }

    public IEnumerator ReduceHealth()
    {
        while (healthComponent.health > 0)
        {
            healthComponent.health -= 1;
            yield return new WaitForSeconds(1f);
        }
    }

    public void SubtractWater(int amount)
    {
        healthComponent.SubtractHealth(amount);
    }

    public void KillPlayer()
    {
        healthComponent.health = 0;
    }

    public void IncreaseMoisture(int amt)
    {
        healthComponent.AddHealth(amt);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Wall"))
        {
            playerAudioSource.PlayOneShot(hitRock, 0.1f);
            StopCoroutine(ReduceHealth());
            KillPlayer();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Water"))
        {
            IncreaseMoisture(10);
            playerAudioSource.PlayOneShot(hitWater, 0.1f);
            Destroy(col.gameObject);
        } else if (col.CompareTag("Rock"))
        {
            speed -= speedReducedBy;
            SubtractWater(waterToReduce);
            playerAudioSource.PlayOneShot(hitRock, 0.1f);
            Destroy(col.gameObject);
        }
    }
}