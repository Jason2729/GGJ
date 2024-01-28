using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Transform doorStart, doorEnd;

    [SerializeField] private float doorSpeed = 1f;
    [SerializeField] private DoorState doorStateOnStart = DoorState.Closed; 
    [SerializeField] private DoorState currentDoorState = DoorState.Closed;
    [Tooltip("if toggled to true, the door will start to close when the plate is not pressed")]
    [SerializeField] private bool isPressurePlateDoor = false;

    private void Start()
    {
        currentDoorState = DoorState.Closed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (currentDoorState != DoorState.Opened && currentDoorState != DoorState.Opening) {
            StopCoroutine(CloseDoor());
            currentDoorState = DoorState.Opening;
            StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isPressurePlateDoor)
        {
            if (currentDoorState != DoorState.Closed && currentDoorState != DoorState.Closing)
            {
                StopCoroutine(OpenDoor());
                currentDoorState = DoorState.Closing;
                StartCoroutine(CloseDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        while((door.transform.position - doorEnd.position).magnitude >= 0 && currentDoorState == DoorState.Opening)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorEnd.position, doorSpeed*Time.deltaTime);
            yield return 0;
        }
        if(currentDoorState == DoorState.Opening)
        {
            currentDoorState = DoorState.Opened;
            door.transform.position = doorEnd.position;

            yield return null;
        }
        else
        {
            yield return null;
        }
        
    }
     private IEnumerator CloseDoor()
    {
        while ((door.transform.position - doorStart.position).magnitude >= 0 && currentDoorState == DoorState.Closing)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorStart.position, doorSpeed* Time.deltaTime);
            yield return 0;
        }
        if(currentDoorState == DoorState.Closing )
        {
            currentDoorState = DoorState.Closed;
            door.transform.position = doorStart.position;
            yield return null;
        }
        else
        {
            yield return null;
        }
    }

    private enum DoorState
    {
        Opening,
        Closing,
        Opened,
        Closed
    }
}
