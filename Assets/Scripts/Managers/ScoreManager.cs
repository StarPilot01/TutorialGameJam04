using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class ScoreManager 
{
    int liverEnergy = 0;
    int maxLiverEnergy = 15; //임의로 최대치 정해놨습니다.
    

    public int LiverEnergy  { get { return liverEnergy; } 
        set { liverEnergy = value;

            if (liverEnergy > maxLiverEnergy)
                liverEnergy = maxLiverEnergy;    

                setLinerValue.Invoke(); } 
    }

    public int MaxLiverEnergy { get { return maxLiverEnergy; } set { maxLiverEnergy = value; } }


    float playTime = 0;
    public float PlayTime { get { return playTime; } set { playTime = value; } }


    public UnityAction<float> setTime;
    public UnityAction setLinerValue;


    public void Init()
    {
      
    }

    public void TicTime()
    {
        playTime += Time.deltaTime;
        setTime.Invoke(playTime);
    }
    
    public void TicEnergy()
    {

    }

}
