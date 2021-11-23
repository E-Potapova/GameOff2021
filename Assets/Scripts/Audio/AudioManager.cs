using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    public static bool Initialized
    {
        get { return initialized; }
    }


    public static void Initialize(AudioSource source)
    {
        // fill with audio file names after getting them
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.DirtStep,
            Resources.Load<AudioClip>(""));
        audioClips.Add(AudioClipName.Jump,
            Resources.Load<AudioClip>(""));
        audioClips.Add(AudioClipName.Dash,
            Resources.Load<AudioClip>(""));
        audioClips.Add(AudioClipName.DoubleJump,
            Resources.Load<AudioClip>(""));
        audioClips.Add(AudioClipName.Hurt,
            Resources.Load<AudioClip>(""));
        audioClips.Add(AudioClipName.Heal,
            Resources.Load<AudioClip>(""));
        audioClips.Add(AudioClipName.Death,
            Resources.Load<AudioClip>(""));
    }

    public static void Play(AudioClipName name)
    {
        // in other scripts: AudioManager.Play(AudioClipName.#name#);
        audioSource.PlayOneShot(audioClips[name]);
    }
}
