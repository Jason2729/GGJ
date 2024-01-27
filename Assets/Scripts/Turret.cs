using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    private Camera _cam;
    [SerializeField, Range(1,100)]private float _rotationSpeed = 10f;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _target;

    void Awake()
    {
        _cam = Camera.main;
    }
    void Start()
    {
        //Start the attack 
        StartCoroutine(attackCoroutine());
    }

    private IEnumerator attackCoroutine()
    {
        while (true)    //continuous attack
        {
            // attack at interval
            yield return new WaitForSeconds(1);
            // spawn projectile
            Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity).Init(transform.up);// where to spawn projectile
        }
    }

    void FixedUpdate()
    {
        // rotate to target position
        var targetPosition =  _cam.ScreenToWorldPoint(_target.transform.position);
        targetPosition.z = 0;
        transform.up = Vector3.MoveTowards(transform.up, targetPosition, _rotationSpeed * Time.deltaTime);        
    }
}
