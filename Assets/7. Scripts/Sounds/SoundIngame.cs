using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SoundSettingTypes
{
    volume,
    volumemovement,
    minSoundDistance,
    maxSoundDistance
}

public class SoundIngame
{
    private List<AudioClip> _clips;

    public SoundIngame()
    {
        _clips = Resources.LoadAll<AudioClip>("Sounds/").ToList();
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
    /// Load the volumes from the settings.json file.
    /// </summary>
    public float LoadSpecificSoundSettings(SoundSettingTypes soundSettingToLoad)
    {
        SimpleJSON.JSONNode node = SimpleJSON.JSON.Parse(Resources.Load<TextAsset>("JSON/Settings/settings").text);
        return node[soundSettingToLoad.ToString()].AsFloat;
    }

    /// <summary>
    /// Add an audiosource to a gameobject and set some default values.
    /// </summary>
    /// <param name="objectToAddTo"></param>
    /// <returns>Returns the audiosource which was added to the gameobject.</returns>
    public AudioSource AddAndSetupAudioSource(GameObject objectToAddTo, SoundSettingTypes volumeSettingTypeToLoad)
    {
        AudioSource source = objectToAddTo.AddComponent<AudioSource>();
        source.loop = false;
        source.playOnAwake = false;
        source.dopplerLevel = 0f;
        source.minDistance = LoadSpecificSoundSettings(SoundSettingTypes.minSoundDistance);
        source.maxDistance = LoadSpecificSoundSettings(SoundSettingTypes.maxSoundDistance);
        float volumeLoaded = LoadSpecificSoundSettings(volumeSettingTypeToLoad);
        source.volume = Mathf.Clamp(volumeLoaded, 0.0f, 1.0f);
        return source;
    }

    /// <summary>
    /// Find and play the sound which has the exact filename as the parameter soundFilename.
    /// </summary>
    /// <param name="source">The audiosource on which to play the sound.</param>
    /// <param name="soundFilename">The exact filename for the sounds which needs to be played.</param>
    /// <param name="shouldIsPlayingBeChecked">If true if will check if it is already playing a sound. If false it will ignore it.</param>
    public void PlaySound(AudioSource source, string soundFilename, bool shouldIsPlayingBeChecked)
    {
        if (!shouldIsPlayingBeChecked || (shouldIsPlayingBeChecked && !source.isPlaying))
        {
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
    /// <param name="shouldIsPlayingBeChecked">If true if will check if it is already playing a sound. If false it will ignore it.</param>
    public void PlaySoundRandom(AudioSource source, string prefixSoundPartFilename, bool shouldIsPlayingBeChecked)
    {
        if (!shouldIsPlayingBeChecked || (shouldIsPlayingBeChecked && !source.isPlaying))
        {
            List<AudioClip> list = _clips.Where(x => x.name.StartsWith(prefixSoundPartFilename)).ToList<AudioClip>();
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
    /// <param name="shouldIsPlayingBeChecked">If true if will check if it is already playing a sound. If false it will ignore it.</param>
    public void PlaySoundIndex(AudioSource source, string prefixSoundPartFilename, int index, bool shouldIsPlayingBeChecked)
    {
        if (!shouldIsPlayingBeChecked || (shouldIsPlayingBeChecked && !source.isPlaying))
        {
            List<AudioClip> list = _clips.Where(x => x.name.StartsWith(prefixSoundPartFilename)).ToList<AudioClip>();
            int indexClip = index >= 0 && index < list.Count ? index : 1;
            source.clip = list[indexClip];
            source.Play();
        }
    }

    public void PlaySoundLoopAndEndtime(AudioSource source, string prefixSoundPartFilename, bool shouldIsPlayingBeChecked, float endTime)
    {
        if (!shouldIsPlayingBeChecked || (shouldIsPlayingBeChecked && !source.isPlaying))
        {
            if(source.time > endTime || source.time <= 0f)
            {
                List<AudioClip> list = _clips.Where(x => x.name.StartsWith(prefixSoundPartFilename)).ToList<AudioClip>();
                source.loop = true;
                source.clip = list[new System.Random().Next(0, list.Count)];
                source.Play();
            }
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
