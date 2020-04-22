
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundSource : System.Object
{
    public AudioSource source;
    public bool canOverride = true;
    public bool indestructable = false;
}

public static class SoundManager
{
    public static List<SoundSource> Sources;

    // Initialize sound manager
    static SoundManager()
    {
    }

    // Add sound to system, will not override if SoundSource.overridable = false;
    public static void AddSound(AudioClip Audio, bool PlayOnce = true)
    {
        bool added = false;
        foreach(SoundSource s in Sources)
        {
            if(!added)
            {
                if(s.canOverride)
                {
                    if (!s.source.isPlaying||s.source.clip == null)
                    {
                        s.source.clip = Audio;
                        s.source.loop = !PlayOnce;
                        s.source.Play();
                        added = true;
                        Debug.Log("[SOUNDMANAGER] Successfully added " + Audio.name + " to AudioSystem (SRC:" + s.source.name + ")");
                    }
                }
            }
            
        }
    }


    // Add sound to system, will override if required.
    public static void OverrideSound(AudioClip Audio, bool PlayOnce = true)
    {
        bool added = false;
        foreach (SoundSource s in Sources)
        {
            if (!added)
            {
                if (!s.indestructable && (s.source.isPlaying || s.source.clip != null))
                {
                    s.source.clip = Audio;
                    s.source.loop = !PlayOnce;
                    s.source.Play();
                    added = true;
                    Debug.Log("[SOUNDMANAGER] Successfully added " + Audio.name + " to AudioSystem (SRC:" + s.source.name + ")");
                }
            }

        }
    }

    public static void ChangeVolume(AudioClip Audio, float Volume)
    {
        foreach (SoundSource s in Sources)
        {
            if (s.source.clip == Audio) s.source.volume = Volume;
        }
    }
}
