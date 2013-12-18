using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    private IDictionary<SoundsPlayer, AudioSource> sounds;

    void Start()
    {
        // Get all the audiosource components in the soundObject. If the name of the files equals the name of the enum
        // then add it to the dictionary. So sounds can be played via an enum.
        sounds = new Dictionary<SoundsPlayer, AudioSource>();
        AudioSource[] soundsArray = GetComponents<AudioSource>();
        
        foreach (SoundsPlayer audio in (SoundsPlayer[])Enum.GetValues(typeof(SoundsPlayer)))
        {
            foreach (AudioSource item in soundsArray)
            {
                if(audio.ToString() == item.clip.name)
                {
                    sounds.Add(audio, item);
                }
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
