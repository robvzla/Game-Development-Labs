using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameUIScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text endText;
    public GameManagerScript gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();    
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Points: " + gameManager.score;
        healthText.text = "Lives: " + gameManager.health;
    }
}
