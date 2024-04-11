using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LobbySettingPopup : MonoBehaviour
{
    [SerializeField]
    Slider _bgmSlider;
    [SerializeField]
    Slider _sfxSlider;

    //float _bgmInitVolume = 0.4f;
    //float _sfxInitVolume = 0.4f;

    public void OnEnable()
    {
        _bgmSlider.value = Managers.SoundManager.BGMInitVolume;
        _sfxSlider.value = Managers.SoundManager.SFXInitVolume;

        _bgmSlider.onValueChanged.AddListener(Managers.SoundManager.SetBGMVolume);
        _sfxSlider.onValueChanged.AddListener(Managers.SoundManager.SetSFXVolume);

        _bgmSlider.onValueChanged.Invoke(_bgmSlider.value);
        _sfxSlider.onValueChanged.Invoke(_sfxSlider.value);
    }

    public void OnClosed()
    {
        this.gameObject.SetActive(false);

        
    }

    
}
