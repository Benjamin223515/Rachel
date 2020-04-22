using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public List<SoundSource> Sources = new List<SoundSource>();

    void Awake()
    {
        foreach (SoundSource s in Sources) s.source.clip = null;
        SoundManager.Sources = Sources;
    }
}
