using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rb;

    // initialize with direction
    public void Init(Vector2 dir)
    {
        _rb = GetComponent<Rigidbody2D>();
        // set velocity of projectile
        _rb.velocity = dir * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // impact effect
        //Instantiate(impactEffect, transform.position, Quaternion.identity);

        // some effect on player
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Slow(1);
        }

        if (collision.tag != "Enemy")
        {
            // destroy projectile
            Destroy(gameObject);
        }           
    }
}
