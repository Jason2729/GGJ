/*
 * Tutorial Used: Point and Shoot / Turret Tutorial - Unity 
 * https://youtu.be/OEJIViea3b0?si=iy07WpX9_xebN0EV
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject explosionEffect;

    // initialize with direction
    public void Init(Vector2 dir)
    {
        _rb = GetComponent<Rigidbody2D>();
        // set velocity of projectile
        _rb.velocity = dir * _speed;
        StartCoroutine(timedDestroy());   // destroy projectile after some time
    }

    private IEnumerator timedDestroy()
    {
        yield return new WaitForSeconds(10);
        // impact effect
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // destroy projectile
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // impact effect
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // some effect on player
        if (collision.tag == "Player")
        {
            collision.GetComponent<BasicPlatformer>().Slow(1);
        }
        // destroy projectile
        Destroy(gameObject);
    }
}
