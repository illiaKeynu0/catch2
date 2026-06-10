using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
        
    [Serializable]public struct SoundEntry
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
    
    public enum SoundType
    {
        PlayerHurt,
        BoulderSplash,
        GemSplash,
        GemCollect,
        Booster
    }

    [SerializeField] private SoundEntry[] soundEntries;
    private Dictionary<SoundType, AudioClip> _sounds;

    private AudioSource _audioSource;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sounds = new Dictionary<SoundType, AudioClip>();

        foreach (var entry in soundEntries)
        {
            _sounds.Add(entry.soundType, entry.audioClip);
        }
    }

    public void PlaySound(SoundType soundType)
    {
        _audioSource.PlayOneShot(_sounds[soundType]);
    }
}
