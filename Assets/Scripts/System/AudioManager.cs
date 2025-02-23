using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Range = UnityEngine.RangeAttribute;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioMixer _audioMixer;
    private AudioSource _seSource;
    [SerializeField]
    private List<AudioData> _soundEffectData = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        _audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        _seSource = gameObject.AddComponent<AudioSource>();
    }
    /// <summary>
    /// éwíËÇµÇΩSEÇçƒê∂Ç∑ÇÈ
    /// </summary>
    /// <param name="seName"></param>
    public void PlaySE(string seName)
    {
        AudioData data = _soundEffectData.Find(d => d.clip.name == seName);
        if (data.clip == null)
        {
            Debug.LogWarning($"{seName}ÇÃSEÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
            return;
        }
        _seSource.PlayOneShot(data.clip, data.volume);
    }
}

[Serializable]
public struct AudioData
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
}