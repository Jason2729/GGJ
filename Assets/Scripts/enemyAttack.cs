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

    private IEnumerator attackCoroutine()
    {
        while (true)    //continuous attack
        {
            // attack at interval
            yield return new WaitForSeconds(1);
            // spawn projectile
            Instantiate(projectile, firePosition.position, Quaternion.identity);// where to spawn projectile
        }
    }


}
