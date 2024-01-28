using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wallHitAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Time.deltaTime - elapsed time for each frame
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void Die()
    {
        Debug.Log("player died!");
        Destroy(gameObject);
    }

    public void Slow(int val)
    {
        Debug.Log("player pied!");
        StartCoroutine(slowCoroutine(val));
    }

    private IEnumerator slowCoroutine(int val)
    {
        // reduce player speed
        playerSpeed -= val;
        // wait for some interval
        yield return new WaitForSeconds(5);
        // restore speed
        Debug.Log("player un-pied!");
        playerSpeed += val;
    }
}
