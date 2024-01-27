using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject impactEffect;
    public GameObject target;

    private Vector3 targetDirection;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = -transform.right * projectileSpeed;
    }

    private void FixedUpdate()
    {
        // get position of target
        targetDirection = target.transform.position;
        // covert 2d vector to 3d vector
        targetDirection = Camera.main.ScreenToWorldPoint(targetDirection); 
        targetDirection.z = 0;
        // set velocity of projectile
        if (targetDirection.x < transform.position.x)
        {
            rigidbody.velocity = transform.right * projectileSpeed;
        }
        else
        {
            rigidbody.velocity = -transform.right * projectileSpeed;
        }
                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // impact effect
        //Instantiate(impactEffect, transform.position, Quaternion.identity);

        // some effect on player
        /*
        if (collision.tag == "Player")
        {
            var playerHealth = GetComponent<playerHealth>();
            if (playerHealth != null)
            {
                playerHealth -= 1;
            
            }
        }
         */
        if (collision.tag != "Enemy")
        {
            // destroy projectile
            Destroy(gameObject);
        }
            
    }
}
