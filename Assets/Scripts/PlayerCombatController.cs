using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{

    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private Vector3 localStartSwingRotation;
    [Tooltip("the angle of the swing area in degrees, max 360 degress")]
    [SerializeField, Range(0.01f, 90f)] private float swingAreaAngle;
    [SerializeField, Range(0.01f, 2f)] private float swingDuration;
    [Tooltip("how fast before the player can input another attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private bool includeRecovery = false;
    [SerializeField] private float recoveryTime;
    [SerializeField] private bool attackOnCooldown = false;
    [SerializeField] private PlayerCombatState playerCombatState;


    private void Start()
    {
        localStartSwingRotation = playerWeapon.transform.localEulerAngles;
        playerCombatState = PlayerCombatState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerCombatState != PlayerCombatState.Attacking && !attackOnCooldown)
            {
                Attack();
            }
            
        }
    }

    void Attack()
    {
        if(playerCombatState != PlayerCombatState.Attacking)
        {
            playerCombatState = PlayerCombatState.Attacking;
            StartCoroutine(Attacking());
            StartCoroutine(AttackCooldown());
        }
        
    }

    void RecoverFromAttack()
    {
        StartCoroutine(Recovering());
    }

    private IEnumerator Attacking()
    {
        playerWeapon.transform.localRotation = Quaternion.Euler(localStartSwingRotation);
        Vector3 endRotation = localStartSwingRotation - new Vector3(0f, 0f, swingAreaAngle);
        float t = 0;
        while(t < swingDuration && playerCombatState == PlayerCombatState.Attacking)
        {
            t += Time.deltaTime;
            // using RotateTowards
            playerWeapon.transform.localRotation = Quaternion.RotateTowards(playerWeapon.transform.localRotation, Quaternion.Euler(endRotation), (swingAreaAngle / swingDuration)*Time.deltaTime);
            yield return null;
        }
        playerWeapon.transform.localEulerAngles = endRotation;
        if(playerCombatState == PlayerCombatState.Attacking)
        { 
            if(includeRecovery) {
                playerCombatState = PlayerCombatState.Recovering;
                StartCoroutine(Recovering());
                yield return new WaitForSeconds(includeRecovery ? recoveryTime : 0f);
            }
            playerCombatState = PlayerCombatState.Idle;
            yield break;
        }
        // was interrupted early
        else
        {
            playerWeapon.transform.localRotation = Quaternion.Euler(localStartSwingRotation);
            yield break;
        }
    }

    private IEnumerator Recovering()
    {
        float t = 0; 
        while(t < recoveryTime && playerCombatState == PlayerCombatState.Recovering)
        {
            t += Time.deltaTime;
            playerWeapon.transform.localRotation = Quaternion.RotateTowards(playerWeapon.transform.localRotation, Quaternion.Euler(localStartSwingRotation), (swingAreaAngle / recoveryTime) * Time.deltaTime);
            yield return null;
        }
        // check if interrupted early
        if(playerCombatState != PlayerCombatState.Recovering)
        {
            playerWeapon.transform.localRotation = Quaternion.Euler(localStartSwingRotation);
        }
        yield break;
    }

    private IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
}

public enum PlayerCombatState
{
    Idle,
    Attacking,
    Recovering,

}
