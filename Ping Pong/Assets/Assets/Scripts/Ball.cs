using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
public class Ball : MonoBehaviour
{
    private Vector3 movement;
    [SerializeField]
    private int playerOneScore;
    [SerializeField]
    private int playerTwoScore;
    private Color[] colors = {Color.red, Color.green, Color.blue};
    private int colorIndex = 0;
    private bool slowDownPowerUpActive = false;
    private AudioSource audioSource;

    public TextMeshProUGUI playerOneText;
    public TextMeshProUGUI playerTwoText;
    public Vector3 spawnPoint;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        this.movement = new Vector3(-9f, 10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += movement * speed;
        playerOneText.text = playerOneScore.ToString();
        playerTwoText.text = playerTwoScore.ToString();
        
        
        colorIndex = (colorIndex + 1) % colors.Length;

        if (playerOneScore > playerTwoScore)
        {
            playerOneText.color = colors[colorIndex];
        }
        
        if (playerTwoScore > playerOneScore)
        {
           
            playerTwoText.color = colors[colorIndex];
        }

        if (slowDownPowerUpActive)
        {
            speed = 0.025f;
            slowDownPowerUpActive = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        movement = Vector3.Reflect(movement, normal);

        if (collision.gameObject.CompareTag("Paddles"))
        {
            speed += 0.010f;
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("SlowDownPowerUp"))
        {
            speed = 0.005f;
            slowDownPowerUpActive = true;
            Destroy(collision.gameObject);
        }
        

        if (collision.gameObject.name == "RightWall")
        {
            playerTwoScore++;
            Debug.Log($"Player Two scored! The score is: {playerOneScore} - {playerTwoScore}" );
            transform.position = spawnPoint;
            this.movement = new Vector3(9f, 10f, 0f);
            speed = 0.025f;

            if (playerTwoScore > 10)
            {
                Debug.Log($"Game Over! Player Two Wins!" );
                playerOneScore = 0;
                playerTwoScore = 0;
            }
        }

        if (collision.gameObject.name == "LeftWall")
        {
            playerOneScore++;
            Debug.Log($"Player One scored! The score is: {playerOneScore} - {playerTwoScore}");
            transform.position = spawnPoint;
            this.movement = new Vector3(-9f, 10f, 0f);
            speed = 0.025f;
            
            if (playerOneScore > 10)
            {
                Debug.Log($"Game Over! Player One Wins!" );
                playerOneScore = 0;
                playerTwoScore = 0;
            }
            
        }

    }
}
