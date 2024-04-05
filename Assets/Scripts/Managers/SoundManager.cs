using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Define;

public class SoundManager
{
    static SoundManager s_instance;

    public static SoundManager Instance { get { return s_instance; } }




    [SerializeField]
    AudioMixer m_AudioMixer;
    [SerializeField]
    AudioSource[] _audioSources = new AudioSource[(int)Define.ESoundType.Max];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    //Slider m_MusicMasterSlider;
    //Slider m_MusicBGMSlider;
    //Slider m_MusicSFXSlider;

    float _MasterVolumeSize = 0.3f;
    float _BGMVolumeSize = 0.3f;
    float _SFXVolumeSize = 0.3f;

    //float _changedMasterVolumeSize;
    //float _changedBGMVolumeSize;
    //float _changedSFXVolumeSize;

    private void Awake()
    {
        


            _audioSources[(int)Define.ESoundType.BGM].volume = _MasterVolumeSize;
            _audioSources[(int)Define.ESoundType.SubBGM].volume = _MasterVolumeSize;
            _audioSources[(int)Define.ESoundType.SFX].volume = _SFXVolumeSize;



    }

    private void Update()
    {

    }

    void MasterChanged(float value)
    {
        _MasterVolumeSize = value;
        BgmChanged(value);
        SFXChanged(value);




    }
    void BgmChanged(float value)
    {
        _audioSources[(int)Define.ESoundType.BGM].volume = value;
        _audioSources[(int)Define.ESoundType.SubBGM].volume = value;

        _BGMVolumeSize = value;


    }
    void SFXChanged(float value)
    {
        _audioSources[(int)Define.ESoundType.SFX].volume = value;

        _SFXVolumeSize = value;
    }



    public void SetSlider()
    {
        


    }

    public void SetMasterVolume(float volume)
    {
        Debug.Log(volume);
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);

        Debug.Log(Mathf.Log10(volume) * 20);

    }

    public void SetBGMVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
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
        //BGM or SubBGM
        path += (type == ESoundType.BGM || type == ESoundType.SubBGM) ? "BGM/" : "SFX/";

        string key = path + fileName;

        if (_audioClips.TryGetValue(key, out AudioClip audioClip))
        {
            return audioClip;
        }



        audioClip = Resources.Load<AudioClip>(key);

        _audioClips.Add(key, audioClip);

        return audioClip;
    }
}
