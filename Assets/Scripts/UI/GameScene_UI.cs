using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene_UI : MonoBehaviour
{
    [SerializeField] 
    Image _liverFillImage;
    [SerializeField] 
    TextMeshProUGUI _elapsedTimeText;
    //[SerializeField] 
    //Image _gameModeIndicatorImage;
    [SerializeField] 
    Image[] _clickModeIndicatorImages;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        
        Managers.GameManager.kumiho.OnLiverEnergyChanged += SetLiverEnergyFill;
    }

    // Update is called once per frame
    void Update()
    {
        SetElapsedTime(Managers.GameManager.ElapsedTime);
    }


    public void SetLiverEnergyFill()
    {
        _liverFillImage.fillAmount =
            (float)Managers.GameManager.kumiho.LiverEnergy / (float)Managers.GameManager.kumiho.MaxLiverEnergy;
    }

    public void SetElapsedTime(float time)
    {
        _elapsedTimeText.text = ((int)time / 60).ToString("D2") + ":" + ((int)time % 60).ToString("D2");
    }
   

    //public void SetClickModeIndicatorVisible(int index)
    //{
    //    modeImage.sprite = modeSprites[index];
    //}


}
