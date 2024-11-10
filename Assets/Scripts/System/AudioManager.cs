using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Range = UnityEngine.RangeAttribute;

public class AudioManager : MonoBehaviour
{
    private AudioMixer _audioMixer;

    [SerializeField]
    private List<AudioData> _soundEffectData = new(); 
}

[Serializable]
public struct AudioData
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
}