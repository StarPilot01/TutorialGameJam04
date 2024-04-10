using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

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
        Managers.GameManager.OnClickModeChanged += VisibleIndicator;
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

    public void VisibleIndicator(EClickMode clickMode)
    {
        for(int i = 0;  i < _clickModeIndicatorImages.Length; i++)
        {
            _clickModeIndicatorImages[i].gameObject.SetActive(false);
        }

        _clickModeIndicatorImages[(int)clickMode].gameObject.SetActive(true);

    }

    //public void SetClickModeIndicatorVisible(int index)
    //{
    //    modeImage.sprite = modeSprites[index];
    //}


}
