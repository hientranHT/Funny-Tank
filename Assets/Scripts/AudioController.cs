using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public AudioSource audioSource;

    public List<AudioClip> audioClips;
    // index  click
    // index  shoot
    // index  hit enemy with coin
    // index  hit enemy
    // index  hit player


    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    public void SetSoundMusic()
    {
        if (UserController.Instance.IsSoundOn == 1)
        {
            UserController.Instance.IsSoundOn = 0;
            UIController.Instance.SetOffUISound();
            audioSource.mute = true;
            return;
        }

        if (UserController.Instance.IsSoundOn == 0)
        {
            UserController.Instance.IsSoundOn = 1;
            audioSource.mute = false;
            UIController.Instance.SetOnUISound();
            return;
        }
    }
}
