using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Based off of Roystan Honks 2D collider code
//https://roystan.net/articles/character-controller-2d/


//ADD A LITTLE attack for the player
public class BasicPlatformer : MonoBehaviour
{
    public static BasicPlatformer Instance;
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
    [SerializeField]
    float coyoteWallTimeMax = 0.2f;
    float coyoteWallTime;

    [SerializeField]
    Renderer currentRenderer;

    [SerializeField]
    float justWallJumpedMax = 0.1f;
    float justWallJumped;

    [SerializeField]
    float jumpForce = 5.5f;
    [SerializeField]
    float wallJumpForce = 8;
    [SerializeField]
    float jetPackMax = 0.1f;
    float jetPack;


    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private bool isWallJump = false;

    [SerializeField] 
    private AudioSource audioSource;
    [SerializeField] 
    private AudioClip jumpAudioClip;

    Vector2 playerVelocity = new Vector2(0, 0);

    float lastWallX = 1f;

    Vector2 fourtyFiveDegree = new Vector2(0.7071f, 0.7071f);

    private BoxCollider2D boxCollider2D;

    private Rigidbody2D playerRigidBody;

    private Collider2D[] overlappingColliders = new Collider2D[16];

    private int randInt = 0;

    private void MoveObject(Vector2 moveHowMuch)
    {
        //playerRigidBody.velocity = moveHowMuch;
        transform.Translate(moveHowMuch * Time.deltaTime);
    }


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        audioSource = GetComponent<AudioSource>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentRenderer = GetComponent<Renderer>();
    }

    public void UpdateWithCard(CardDataScriptableObject cardDataScriptableObject)
    {
        int tempRandom;
        do
        {
            tempRandom = Mathf.FloorToInt(Random.Range(0, 3));
        } while (tempRandom == randInt);

        randInt = tempRandom;

        currentRenderer.material.SetFloat("_FaceChoice", randInt);
        maxSpeed *= cardDataScriptableObject.moveSpeedModifier;
        speedAccel *= cardDataScriptableObject.moveSpeedModifier;

        jumpForce *= cardDataScriptableObject.jumpHeightModifier;
        wallJumpForce *= cardDataScriptableObject.jumpHeightModifier;
    }

    // Update is called once per frame
    //Keep it update cuz we want to process input every frame
    void Update()
    {
        //The classic frame chucking technique when it gets to laggy just dont calculate anything
        //Prevents the player from falling through the floor
        if (Time.deltaTime < 0.1f)
        {

            if (coyoteTime < 0) {isGrounded = false;}
            else {coyoteTime -= Time.deltaTime;}

            if (coyoteWallTime < 0) {isWallJump = false; }
            else { coyoteWallTime -= Time.deltaTime; }

            if (justWallJumped > 0) { justWallJumped -= Time.deltaTime; }
            
            //"Friction" Equations
            if ((Input.GetAxis("Horizontal") > 0 || isGrounded) && playerVelocity.x < 0)
            {
                playerVelocity.x = Mathf.Min(playerVelocity.x + frictionForce * Time.deltaTime, 0);
            }

            if ((Input.GetAxis("Horizontal") < 0 || isGrounded) && playerVelocity.x > 0)
            {
                playerVelocity.x = Mathf.Max(playerVelocity.x - frictionForce * Time.deltaTime, 0);
            }


            //Input Conditions
            playerVelocity.x = Mathf.Clamp(playerVelocity.x + Input.GetAxis("Horizontal") * speedAccel * Time.deltaTime, maxSpeed * -1, maxSpeed);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                audioSource.PlayOneShot(jumpAudioClip);
                isGrounded = false;
                playerVelocity.y = jumpForce;
            }
            else if (isWallJump && Input.GetKeyDown(KeyCode.Space))
            {
                justWallJumped = justWallJumpedMax;
                audioSource.PlayOneShot(jumpAudioClip);
                isWallJump = false;
                fourtyFiveDegree.x = lastWallX < 0 ? Mathf.Abs(fourtyFiveDegree.x) * -1 : Mathf.Abs(fourtyFiveDegree.x);
                
                playerVelocity = fourtyFiveDegree * wallJumpForce;
            }
            //Add player extra jump height capibilities && Gravity
            if (Input.GetKey(KeyCode.Space) && jetPack > 0 && playerVelocity.y > 0)
            {
                jetPack -= Time.deltaTime;
            }
            else
            {
                playerVelocity.y = Mathf.Max(playerVelocity.y + gravity * Time.deltaTime, terminalVelocity);
            }

            //Debug.Log(playerVelocity);
            //Move player
            MoveObject(playerVelocity);

            int collidersAmount = Physics2D.OverlapBoxNonAlloc(transform.position, boxCollider2D.size, 0, overlappingColliders);
            for (int i = 0; i < collidersAmount; i++)
            {
                if (!(overlappingColliders[i].gameObject.name == gameObject.name))
                {

                    ColliderDistance2D colliderDistance = overlappingColliders[i].Distance(boxCollider2D);
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                    //Alright change this this method sucks for telling what direction it came from
                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 85)
                    { 
                        if (playerVelocity.y < 0)
                        {
                            coyoteTime = coyoteTimeMax;
                            jetPack = jetPackMax;
                            isGrounded = true;
                        }
                        playerVelocity.y = 0;
                    }

                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) >= 85 && Vector2.Angle(colliderDistance.normal, Vector2.up) < 180)
                    {
                        lastWallX = colliderDistance.normal.x;
                        coyoteWallTime = coyoteWallTimeMax;

                        if (justWallJumped <= 0)
                            { playerVelocity.x = 0; }

                        isWallJump = true;
                    }
                }
            }
        }
    }

    // Slowing function (from projectile1)
    public void Slow(int val)
    {
        Debug.Log("player slowed!");
        StartCoroutine(slowCoroutine(val));
    }

    private IEnumerator slowCoroutine(int val)
    {
        // reduce player speed
        maxSpeed -= val;
        // wait for some interval
        yield return new WaitForSeconds(5);
        // restore speed
        Debug.Log("player un-slowed!");
        maxSpeed += val;
    }
}

