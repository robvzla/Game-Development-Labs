using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
	public float maxSpeed;
	public float acceleration;
	public Rigidbody2D myRb;
	public float jumpForce;
	public bool isGrounded;
	public float secondaryJumpForce;
	public float secondaryJumpTime;
	public bool secondaryJump;
	public Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		myRb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));

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
			StartCoroutine(SecondaryJump());
		}

		if(isGrounded == false && Input.GetButton("Jump"))
		{
			myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force); // while the jump button is held
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		isGrounded = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		isGrounded = false;
	}

	IEnumerator SecondaryJump()
	{
		secondaryJump = true;
		yield return new WaitForSeconds(secondaryJumpTime);
		secondaryJump = false;
		yield return null;
	}

}
