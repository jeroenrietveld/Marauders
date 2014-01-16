﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class SoundIngame
{
    private List<AudioClip> _clips;
    private float _volume;
    public SoundIngame()
    {
        _clips = Resources.LoadAll<AudioClip>("Sounds/").ToList();
        _volume = GetVolume();
        //WriteVolume(0.333333f);
    }

    /// <summary>
    /// Not working yet.
    /// </summary>
    /// <param name="volume"></param>
    public void WriteVolume(float volume)
    {
        SimpleJSON.JSONNode node = SimpleJSON.JSON.Parse(Resources.Load<TextAsset>("JSON/Settings/settings").text);
        node["volume"] = "0.1111";
        
        using(var filestream = File.Create("Assets/Resources/JSON/Settings/settings.json"))
        {
            BinaryWriter bw = new BinaryWriter(filestream);
            //node.Serialize(bw);
            node.SaveToStream(filestream);
        }
    }

    /// <summary>
    /// Read the volume from the settings.json file.
    /// </summary>
    /// <returns>The volume defined in the settings.json file.</returns>
    public float GetVolume()
    {
        SimpleJSON.JSONNode node = SimpleJSON.JSON.Parse(Resources.Load<TextAsset>("JSON/Settings/settings").text);
        float volume = node["volume"].AsFloat;
        Debug.Log("GetVolume: " + volume);
        return volume; 
    }

    /// <summary>
    /// Add an audiosource to a gameobject and set some default values.
    /// </summary>
    /// <param name="objectToAddTo"></param>
    /// <returns>Returns the audiosource which was added to the gameobject.</returns>
    public AudioSource AddAndSetupAudioSource(GameObject objectToAddTo)
    {
        AudioSource source = objectToAddTo.AddComponent<AudioSource>();
        source.volume = _volume;
        source.loop = false;
        source.playOnAwake = false;
        source.minDistance = 200f;
        source.maxDistance = 250f;
        return source;
    }

    /// <summary>
    /// Find and play the sound which has the exact filename as the parameter soundFilename.
    /// </summary>
    /// <param name="source">The audiosource on which to play the sound.</param>
    /// <param name="soundFilename">The exact filename for the sounds which needs to be played.</param>
    public void PlaySound(AudioSource source, string soundFilename)
    {
        if(!source.isPlaying || source.isPlaying)
        {
            source.volume = _volume;
            source.clip = _clips.First(x => x.name == soundFilename);
            source.Play();
        }
    }

    /// <summary>
    /// Find and play one of the sounds that are found. Given the prefix prefixSoundPartFilename in order
    /// to find all of the audioclips which start with the same prefix. It chooses a random sound.
    /// </summary>
    /// <param name="source">The audiosource on which to play the sound.</param>
    /// <param name="prefixSoundPartFilename">The prefix which loads all of the audioclips which start with this value.</param>
    public void PlaySoundRandom(AudioSource source, string prefixSoundPartFilename)
    {
        if (!source.isPlaying || source.isPlaying)
        {
            List<AudioClip> list = _clips.Where(x => x.name.StartsWith(prefixSoundPartFilename)).ToList<AudioClip>();
            source.volume = _volume;
            source.clip = list[new System.Random().Next(0, list.Count)];
            source.Play();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The audiosource on which to play the sound.</param>
    /// <param name="prefixSoundPartFilename">The prefix which loads all of the audioclips which start with this value.</param>
    /// <param name="index">The index used to play a audioclip from the resultlist.</param>
    public void PlaySoundIndex(AudioSource source, string prefixSoundPartFilename, int index)
    {
        if (!source.isPlaying || source.isPlaying)
        {
            List<AudioClip> list = _clips.Where(x => x.name.StartsWith(prefixSoundPartFilename)).ToList<AudioClip>();
            source.volume = _volume;
            int indexClip = index >= 0 && index < list.Count ? index : 1;
            source.clip = list[indexClip];
            source.Play();
        }
    }

    /// <summary>
    /// Stop the sound from playing.
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if(source.isPlaying)
        {
            source.Stop();
        }
    }

    /// <summary>
    /// Pause the sound from playing.
    /// </summary>
    /// <param name="source"></param>
    public void PauseSound(AudioSource source)
    {
        if(source.isPlaying)
        {
            source.Pause();
        }
    }
}
