using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    AudioClip[] audioClips;

    [SerializeField]
    AudioSource[] audioSources;

    public enum Sounds
    {
        die,
        jump,
        getScore,
        silk,
        wind
    }

    public static bool isSpawned = false;

    // Use this for initialization
    void Start()
    {
        if (!isSpawned)
        {
            DontDestroyOnLoad(gameObject);
            isSpawned = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    AudioSource GetAudioSourceFree()
    {
        int i = 0;

        while (audioSources[i].isPlaying)
        {
            i++;

            if (i == audioSources.Length) i = 0;
        }

        return audioSources[i];
    }

    public void Play(Sounds enumClip)
    {
        AudioSource audioSource = GetAudioSourceFree();

        if (!audioSource.isPlaying)
        {
            switch (enumClip)
            {
                case Sounds.die:
                    audioSource.clip = audioClips[1];
                    break;
                case Sounds.jump:
                    audioSource.clip = audioClips[2];
                    break;
                case Sounds.getScore:
                    audioSource.clip = audioClips[0];
                    break;
                case Sounds.silk:
                    audioSource.clip = audioClips[4];
                    break;
                case Sounds.wind:
                    audioSource.clip = audioClips[5];
                    break;

                default:
                    break;
            }

            audioSource.Play();
        }
    }
}
