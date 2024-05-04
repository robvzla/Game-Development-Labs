using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshAI : MonoBehaviour
{
	public Transform player;
    public Animator animator;
    public NavMeshAgent agent;
    public float currentVelocity;
	// Start is called before the first frame update
	void Start()
	{
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
        currentVelocity = agent.velocity.magnitude;
        animator.SetFloat("velocity", currentVelocity);
		// Move towards the player using navmesh
        agent.destination = player.position;
	}
}
