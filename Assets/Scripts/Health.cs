using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField, Range(1,100)] private int maxHp = 10;
    private GameObject Agent;
    public int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        Agent = gameObject;
    }
    public void takeDamage(int damage)
    {
        Debug.Log("ow!");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            if (Agent.GetComponent<PlayerController>() != null)
            {
                Agent.GetComponent<PlayerController>().Die();
            }
            if (Agent.GetComponent<EnemyAI>() != null) 
            {
                Agent.GetComponent<EnemyAI>().Die();
            }
        }
    }
}
