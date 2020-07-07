using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    float delayBetween;

    bool canPlay;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    public void Play()
    {
        if (!canPlay)
            return;

        int clipIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }
}
