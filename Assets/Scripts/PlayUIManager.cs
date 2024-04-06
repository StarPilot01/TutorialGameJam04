using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;


public class PlayUIManager : MonoBehaviour
{
    [SerializeField] Image liner;
    [SerializeField] Text timeText;
    [SerializeField] Image modeImage;
    [SerializeField] Sprite[] modeSprites;
    
    void Start()
    {
        Managers.ScoreManager.setTime += SetTimeText;
        Managers.ScoreManager.setLinerValue += SetlinerValue;
        Managers.GameManager.KumihoUI += SetMode;
    }

    void Update()
    {
        Managers.ScoreManager.TicTime();
    }

    public void SetlinerValue()
    {
        liner.fillAmount = 
            (float)Managers.ScoreManager.LiverEnergy / (float)Managers.ScoreManager.MaxLiverEnergy;
    }

    public void SetTimeText(string str)
    {
        timeText.text = str;
    }
    public void SetTimeText(float time)
    {
        timeText.text = ((int)time / 60).ToString("D2") + ":" + ((int)time % 60).ToString("D2");
    }

    public void SetMode(int index)
    {
        modeImage.sprite = modeSprites[index];
    }
}
