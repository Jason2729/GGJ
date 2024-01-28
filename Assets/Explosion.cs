using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("explode!");
        audioSource = GetComponent<AudioSource>();
        //Start effect
        StartCoroutine(explodeEffect());
    }

    private IEnumerator explodeEffect()
    {
        // Sound
        audioSource.PlayOneShot(explosionAudioClip);
        // Wait for audio
        yield return new WaitForSeconds(1);
        // Destroy this object
        Destroy(gameObject);
    }

}
