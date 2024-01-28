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
    [SerializeField] private GameObject _target;

    void Start()
    {
        //Start the attack 
        StartCoroutine(attackCoroutine());
    }

    private IEnumerator attackCoroutine()
    {
        while (_target != null)    //continuously attack target
        {
            // attack at interval
            yield return new WaitForSeconds(4);
            // spawn random projectile
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
        if (_target != null)
        {
            var targetPosition = _target.transform.position;
            targetPosition.z = 0;
            // rotate to target position
            transform.up = Vector3.MoveTowards(transform.up, targetPosition, _rotationSpeed * Time.deltaTime);
        }
    }
}
