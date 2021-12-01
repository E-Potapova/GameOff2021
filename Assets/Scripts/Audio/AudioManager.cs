using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();
    static Dictionary<AudioClipName, float> audioClipVolumes = new Dictionary<AudioClipName, float>();

    public static bool Initialized
    {
        get { return initialized; }
    }


    public static void Initialize(AudioSource source)
    {
        // fill with audio file names after getting them
        initialized = true;
        audioSource = source;

        audioClips.Add(AudioClipName.Step, Resources.Load<AudioClip>("SFX/Footsteps/Footstep_Dirt_03"));
        audioClipVolumes.Add(AudioClipName.Step, 0.4f);

        audioClips.Add(AudioClipName.Land, Resources.Load<AudioClip>("SFX/gravel"));
        audioClipVolumes.Add(AudioClipName.Land, 0.5f);

        audioClips.Add(AudioClipName.Dash, Resources.Load<AudioClip>("SFX/Dash/swosh-05"));
        audioClipVolumes.Add(AudioClipName.Dash, 0.5f);

        audioClips.Add(AudioClipName.DoubleJump, Resources.Load<AudioClip>("SFX/"));
        audioClipVolumes.Add(AudioClipName.DoubleJump, 0.5f);

        audioClips.Add(AudioClipName.Hurt, Resources.Load<AudioClip>("SFX/Damage/hit14.mp3"));
        audioClipVolumes.Add(AudioClipName.Hurt, 0.5f);

        audioClips.Add(AudioClipName.Heal,Resources.Load<AudioClip>("SFX/Heal/healspell1"));
        audioClipVolumes.Add(AudioClipName.Heal, 0.7f);

        audioClips.Add(AudioClipName.Death, Resources.Load<AudioClip>("SFX/death_bell_sound_effect"));
        audioClipVolumes.Add(AudioClipName.Death, 0.8f);

        audioClips.Add(AudioClipName.UnlockAbility, Resources.Load<AudioClip>("SFX/133008__cosmicd__annulet-of-absorption"));
        audioClipVolumes.Add(AudioClipName.UnlockAbility, 0.8f);
    }

    public static void Play(AudioClipName name)
    {
        // in other scripts: AudioManager.Play(AudioClipName.#name#);
        audioSource.PlayOneShot(audioClips[name], audioClipVolumes[name]);
    }

    public static void PlayMusic()
    {
        audioSource.Play();
    }

    public static void Pause()
    {
        audioSource.Pause();
    }

    public static void UnPause()
    {
        audioSource.UnPause();
    }
}
