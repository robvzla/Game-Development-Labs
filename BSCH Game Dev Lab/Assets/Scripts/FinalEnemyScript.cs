using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyScript : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
    }

    public State enemyAIState;
    public float moveSpeed;
    private float speed;
    private Rigidbody2D _myRb;
    public Animator anim;

    public Transform leftLimit;
    public Transform rightLimit;
    private bool movingRight = true;
    public GameManagerScript gameManager;
    public bool onTop;

    void Start()
    {
        enemyAIState = State.Patrol;
        _myRb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();    
    }

    void Update()
    {
        _myRb.velocity = new Vector2(speed, _myRb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(_myRb.velocity.x));

        switch (enemyAIState)
        {
            case State.Patrol:
                Patrol();
                break;
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            if (transform.position.x > rightLimit.position.x)
            {
                movingRight = false;
                anim.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                speed = moveSpeed;
            }
        }
        else
        {
            if (transform.position.x < leftLimit.position.x)
            {
                movingRight = true;
                anim.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                speed = -moveSpeed;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y < 0)
        {
            // Player is colliding from the top
            onTop = true;
            anim.SetBool("isHit", true);
            StartCoroutine(WaitAndDie());
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.ReduceLives(1);
            moveSpeed = 0;
            StartCoroutine(Wait());
        }
    }
    
    private IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(0.32f);
        Destroy(gameObject);
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
        moveSpeed = 2;
    }
}