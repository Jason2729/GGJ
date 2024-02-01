using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    //[SerializeField] private Animator playerAnimator;

    private Health healthScript;

    // Start is called before the first frame update
    void Start()
    {
        //playerAnimator = GetComponent<Animator>();
        healthScript = GetComponent<Health>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void Die()
    {
        Debug.Log("player died!");
        // For now just restore hp
        healthScript.currentHp = 10;
        //Destroy(gameObject);
    }

    /*public void attack()
    {
        Debug.Log("player attacking!");
        // Attack collison with enemy tag
        attackPoint.OnTriggerEnter2D;
        // some effect on player
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().takeDamage(1);
        }
        //Destroy(gameObject);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // some effect on player
        if (collision.tag == "Enemy" && Input.GetKey(KeyCode.Q))
        {
            collision.GetComponent<Health>().takeDamage(1);
        }
    }

}
