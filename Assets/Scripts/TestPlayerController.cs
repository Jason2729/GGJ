using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
    }

    private void HandleGravity()
    {

    }

    private void HandleMovement()
    {

    }
}
