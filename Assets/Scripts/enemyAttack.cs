using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;


    void Start()
    {
        //Start the attack 
        StartCoroutine(attackCoroutine());
     }

    IEnumerator attackCoroutine()
    {
        while (true)
        {
            // attack at interval
            yield return new WaitForSeconds(1);
            // spawn projectile
            Instantiate(projectile, firePosition.position, firePosition.rotation);// where to spawn projectile
        }
    }

    /*private IEnumerator fireProjectile()
    {
        // attack at interval
        yield return new WaitForSeconds(1); //resumes coroutine after specified time
        // spawn projectile
        // where to spawn projectile
    }*/
}
