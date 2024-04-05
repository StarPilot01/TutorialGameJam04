using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

public class ScoreManager 
{
    int liverEnergy = 0;
    int maxLiverEnergy = 15;

    public int LiverEnergy  { get { return liverEnergy; } set { liverEnergy = value; } }
    public int MaxLiverEnergy { get { return maxLiverEnergy; } set { maxLiverEnergy = value; } }




    public void Init()
    {
      
    }
    

}
