using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField, Range(1,100)] private int maxHp = 10;
    [SerializeField] private GameObject Agent;
    public int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }
    public void takeDamage(int damage)
    {
        Debug.Log("ow!");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Agent.GetComponent<PlayerController>().Die();
        }
    }
}
