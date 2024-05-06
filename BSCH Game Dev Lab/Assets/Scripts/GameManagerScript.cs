using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public float health;
	public float score;	
	public Transform spawnPoint;

	public void awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Start").transform;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            // Destroy the player
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

	public void AddScore(float scoreToAdd)
	{
		score += scoreToAdd;
	}
    
    public void ReduceLives(float healthToReduce)
    {
        health -= healthToReduce;
    }
}