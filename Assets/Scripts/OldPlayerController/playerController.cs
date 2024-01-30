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

}
