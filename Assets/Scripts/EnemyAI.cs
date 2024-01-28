/*
 * Tutorial Used: 2D follow AI with Unity - [Tutorial]
 * https://youtu.be/jlklOd8PEQE?si=uwTTbTJMIwchIx7r
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField, Range(0,11)] private float awareness = 3f;

    private Transform _target;

    void Awake()
    {
        //Find player 
        _target = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate()
    {
        // Some range of awareness
        if (Mathf.Abs(_target.position.x - transform.position.x) < awareness && Mathf.Abs(_target.position.y - transform.position.y) < awareness)
        {
            // Face player
            var dir = _target.position - transform.position;
            transform.up = Vector3.MoveTowards(transform.up, dir, _rotationSpeed * Time.deltaTime);
            // Move towards player w/ overshoot
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, _speed * Time.deltaTime);
        }
    }

    public void Die()
    {
        Debug.Log("enemy died!");
        Destroy(gameObject);
    }
}
