using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

/// <summary>
/// This script needs the be added to the marauder prefabs so the source of the audio is from the player.
/// </summary>
public class SoundPlayer : MonoBehaviour {

    private IDictionary<SoundsPlayer, AudioSource> sounds;

    void Start()
    {
        sounds = new Dictionary<SoundsPlayer, AudioSource>();
        LoadSounds();
    }

    /// <summary>
    /// Load the sounds which are in de Resources/Sounds/Character folder if it matches with the enum.
    /// The enum name and corresponding sound name need to be the same.
    /// </summary>
    public void LoadSounds()
    {
        foreach (SoundsPlayer audio in (SoundsPlayer[])Enum.GetValues(typeof(SoundsPlayer)))
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Characters/" + audio.ToString());
            if(!clip.Equals(null))
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                // Set the minimum distance to a large number so the audio will always play load at the camera.
                audioSource.minDistance = 200f;
                audioSource.minDistance = 210f;
                audioSource.playOnAwake = false;
                sounds.Add(audio, audioSource);
            }
        }
    }


    public void PlaySound(SoundsPlayer soundType)
    {
        if(sounds.ContainsKey(soundType))
        {
            AudioSource source = sounds[soundType];
            if (!source.isPlaying) { source.Play(); }
        }
    }

    public void PauseSound(SoundsPlayer soundType)
    {
        if (sounds.ContainsKey(soundType))
        {
            AudioSource source = sounds[soundType];
            if (source.isPlaying) { source.Pause(); }
        }
    }

    public void StopSound(SoundsPlayer soundType)
    {
        if (sounds.ContainsKey(soundType))
        {
            AudioSource source = sounds[soundType];
            if (source.isPlaying) { source.Stop(); }
        }
    }
    
}
