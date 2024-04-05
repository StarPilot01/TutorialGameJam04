using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UIManager
{
    Image liner;
    Text timeText;

    public void Init()
    {
        
    }

    public void SetlinerValue(float curValue, float maxValue = 1)
    {
        liner.fillAmount = curValue / maxValue;
    }

    public void SetTimeText(string str)
    {
        timeText.text = str;
    }
}
