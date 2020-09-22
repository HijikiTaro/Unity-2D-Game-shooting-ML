using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip boomSE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(boomSE);
        Destroy(gameObject, 0.5f);
    }


}
