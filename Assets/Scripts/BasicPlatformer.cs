using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Based off of Roystan Honks 2D collider code
//https://roystan.net/articles/character-controller-2d/


//ADD A LITTLE attack for the player
public class BasicPlatformer : MonoBehaviour
{
    [SerializeField]
    float terminalVelocity = -9.8f;
    [SerializeField]
    float gravity = -9.8f;
    [SerializeField]
    float maxSpeed = 5;
    [SerializeField]
    float speedAccel = 20;
    [SerializeField]
    float frictionForce = 20;
    [SerializeField]
    float coyoteTimeMax = 0.2f;
    float coyoteTime;
    //[SerializeField]
    //float friction = 9;
    [SerializeField]
    float jumpForce = 8;

    [SerializeField]
    private bool isGrounded = false;
    private bool canWallJump = false;

    Vector2 playerVelocity = new Vector2(0, 0);

    private BoxCollider2D boxCollider2D;

    private Rigidbody2D playerRigidBody;

    private Collider2D[] overlappingColliders = new Collider2D[16];

    private void MoveObject(Vector2 moveHowMuch)
    {
        //playerRigidBody.velocity = moveHowMuch;
        transform.Translate(moveHowMuch * Time.deltaTime);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //The classic frame chucking technique when it gets to laggy just dont calculate anything
        //Prevents the player from falling through the floor
        if (Time.deltaTime < 0.1f)
        {
            coyoteTime -= Time.deltaTime;
            if (coyoteTime < 0)
            {
                isGrounded = false;
            }

            //"Friction" Equations
            if (Input.GetAxis("Horizontal") >= 0 && playerVelocity.x < 0)
            {
                playerVelocity.x = Mathf.Min(playerVelocity.x + frictionForce * Time.deltaTime, 0);
            }

            if (Input.GetAxis("Horizontal") <= 0 && playerVelocity.x > 0)
            {
                playerVelocity.x = Mathf.Max(playerVelocity.x - frictionForce * Time.deltaTime, 0);
            }


            //Input Conditions
            playerVelocity.x = Mathf.Clamp(playerVelocity.x + Input.GetAxis("Horizontal") * speedAccel * Time.deltaTime, maxSpeed * -1, maxSpeed);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                playerVelocity.y = jumpForce;
            }
            //Gravity
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = Mathf.Max(playerVelocity.y + gravity * Time.deltaTime, terminalVelocity);
            }

            //Move player
            MoveObject(playerVelocity);

            int collidersAmount = Physics2D.OverlapBoxNonAlloc(transform.position, boxCollider2D.size, 0, overlappingColliders);
            for (int i = 0; i < collidersAmount; i++)
            {
                if (!(overlappingColliders[i].gameObject.name == gameObject.name))
                {

                    ColliderDistance2D colliderDistance = overlappingColliders[i].Distance(boxCollider2D);
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                    Debug.Log("Move to : " + (colliderDistance.pointA - colliderDistance.pointB) + " Velocity of Player " + playerVelocity + " Time.deltatime " + Time.deltaTime);

                    //Alright change this this method sucks for telling what direction it came from
                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90)
                    {
                        //Debug.Log(colliderDistance.normal + " " + Vector2.Angle(colliderDistance.normal, Vector2.up) + " Stopping Vertical " + (colliderDistance.pointA - colliderDistance.pointB));
                        if (playerVelocity.y < 0)
                        {
                            coyoteTime = coyoteTimeMax;
                            isGrounded = true;
                        }
                        playerVelocity.y = 0;
                    }

                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) >= 90)
                    {
                        //Debug.Log(Vector2.Angle(colliderDistance.normal, Vector2.up) + " Stopping Horizontal " + (colliderDistance.pointA - colliderDistance.pointB));
                        playerVelocity.x = 0;
                        canWallJump = true;
                    }
                }
            }
        }
    }
}

