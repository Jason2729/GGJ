/*
 * Tutorial Used: Point and Shoot / Turret Tutorial - Unity 
 * https://youtu.be/OEJIViea3b0?si=iy07WpX9_xebN0EV
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    [SerializeField, Range(1,100)]private float _rotationSpeed = 10f;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Projectile1 _projectilePrefab1;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField, Range(0, 11)] private float awareness = 5f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootAudioClip;

    private Transform _target;

    void Awake()
    {
        //Find player 
        _target = FindObjectOfType<PlayerController>().transform;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Start the attack 
        StartCoroutine(attackCoroutine());
    }

    private IEnumerator attackCoroutine()
    {
        // Some range of awareness
        while (_target != null)    //continuously attack target
        {
            // attack at interval
            yield return new WaitForSeconds(3);
            // spawn random projectile
            audioSource.PlayOneShot(shootAudioClip);
            if (Random.Range(1, 3) == 1)
            {
                Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity).Init(transform.up);// where to spawn projectile
            }
            else
            {
                Instantiate(_projectilePrefab1, _spawnPoint.position, Quaternion.identity).Init(transform.up);// where to spawn projectile
            }
            
        }
    }

    void FixedUpdate()
    {
        // find target
        if (_target != null && (Mathf.Abs(_target.position.x - transform.position.x) < awareness && Mathf.Abs(_target.position.y - transform.position.y) < awareness))
        {
            var targetPosition = _target.transform.position;
            targetPosition.z = 0;
            targetPosition.y += 1;  // correcting a little
            // rotate to target position
            transform.up = Vector3.MoveTowards(transform.up, targetPosition, _rotationSpeed * Time.deltaTime);
        }
    }
}
