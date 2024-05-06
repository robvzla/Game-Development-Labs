using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowboyControllerScript : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public Rigidbody2D myRb;
    public float jumpForce;
    public bool isGrounded;
    public Animator anim;
    public GameManagerScript gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));
      
		
        //detect player direction and flip the sprite
        if (myRb.velocity.x > 0.1f)
        {
            anim.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (myRb.velocity.x < -0.1f)
        {
            anim.transform.localScale = new Vector3(-1, 1, 1);
        }	
        
        //animation flip
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            anim.transform.localScale = new Vector3(1, 1, 1);
        }
        if(Input.GetAxis("Horizontal") < -0.1f)
        {
            anim.transform.localScale = new Vector3(-1, 1, 1);
        }
			
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Mathf.Abs(myRb.velocity.x)< maxSpeed)
        {
            myRb.AddForce(new Vector2(Input.GetAxis("Horizontal") *  acceleration, 0), ForceMode2D.Force);
        }

        // Jump code
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = true;
        anim.SetBool("isGrounded", false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
        anim.SetBool("isGrounded", true);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && Mathf.Abs(collision.contacts[0].normal.x) > 0.1f)
        {
            anim.SetBool("isDead", true);
            StartCoroutine(WaitAndRespawn());
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            anim.SetBool("isDead", true);
            StartCoroutine(WaitAndRespawn());
        }
    }

    private void Respawn()
    {
        transform.position = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>()
            .spawnPoint.position;
        anim.SetBool("isDead", false); 
    }
    
    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(0.32f);
        Respawn();
    }
}
