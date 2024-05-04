using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavMesh : MonoBehaviour
{
    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public float currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = agent.velocity.magnitude;
        animator.SetFloat("velocity", currentVelocity);
        // Move towards the mouse click using navmesh
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
    }
}
