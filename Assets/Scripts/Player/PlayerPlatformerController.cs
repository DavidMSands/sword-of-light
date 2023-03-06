using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject {
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private bool facingRight = true;
    public GameObject enemy;
	private float moveInput;
    [SerializeField] private GameObject graphic;
    [SerializeField] private Animator animator;
    [SerializeField] private bool jumping;
    // public CameraEffects cameraEffect;

    // private static PlayerPlatformerController instance;
	// public static PlayerPlatformerController Instance{
	// 	get 
	// 	{ 
	// 		if (instance == null) instance = GameObject.FindObjectOfType<PlayerPlatformerController>(); 
	// 		return instance;
	// 	}
	// }

    void Start(){
		
	}
    
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        moveInput = Input.GetAxis("Horizontal");

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (facingRight == false && moveInput > 0)
			{
				Flip();
			}
			else if (facingRight == true && moveInput < 0) {
				Flip();
			}

        if(graphic) {
            if(move.x > 0.01f) {
                if(graphic.transform.localScale.x == 1) {
                    graphic.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                } else if (move.x < -0.01f) {
                    graphic.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
            }
        }

       if (Input.GetKeyDown (KeyCode.X)) {
				animator.SetTrigger ("attack");
       }
        

        if(animator) {
            animator.SetBool ("grounded", grounded);
            animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        }
        targetVelocity = move * maxSpeed;
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        graphic.transform.localScale = scaler;
    }
}
