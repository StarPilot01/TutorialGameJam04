using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Define;

public class SoundManager
{

    AudioSource[] _audioSources = new AudioSource[(int)Define.ESoundType.Max];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private GameObject _soundRoot = null;

    public float BGMInitVolume { get; } = 0.4f;
    public float SFXInitVolume { get; } = 0.4f;
    public void Init()
    {
        if(_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            if (_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                UnityEngine.Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = System.Enum.GetNames(typeof(Define.ESoundType));
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSources[(int)Define.ESoundType.BGM].loop = true;
                _audioSources[(int)Define.ESoundType.BGM].volume = BGMInitVolume;
                _audioSources[(int)Define.ESoundType.SubBGM].loop = true;
                _audioSources[(int)Define.ESoundType.SubBGM].volume = SFXInitVolume;
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        _audioClips.Clear();
    }

    public void Play(ESoundType type, AudioClip audioClip)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == ESoundType.BGM)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();


        }
        else if (type == ESoundType.SubBGM)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (type == ESoundType.SFX)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    public void Play(ESoundType type, string fileName)
    {
        AudioSource audioSource = _audioSources[(int)type];
        AudioClip audioClip = LoadAudioClip(type, fileName);
        if (type == ESoundType.BGM)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();


        }
        else if (type == ESoundType.SubBGM)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (type == ESoundType.SFX)
        {
            audioSource.PlayOneShot(audioClip);
        }



    }

    public void Stop(ESoundType type)
    {
        _audioSources[(int)type].Stop();
    }

    AudioClip LoadAudioClip(ESoundType type, string fileName)
    {
        string path = "Sounds/";
        
        path += type.ToString() +"/";

        string key = path + fileName;

        if (_audioClips.TryGetValue(key, out AudioClip audioClip))
        {
            return audioClip;
        }



        audioClip = Resources.Load<AudioClip>(key);

        _audioClips.Add(key, audioClip);

        return audioClip;
    }

    public void SetBGMVolume(float volume)
    {
        _audioSources[(int)Define.ESoundType.BGM].volume = volume;
    }
    
    public void SetSFXVolume(float volume)
    {
        _audioSources[(int)Define.ESoundType.SFX].volume = volume;
    }
}
