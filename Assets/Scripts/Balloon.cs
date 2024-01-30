using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("collision with player");
            UIManager.Instance.CollectBalloon();
            Destroy(this.gameObject);
        }
    }
}
