using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanquin : MonoBehaviour , ISpawnable
{
    HumanController _targetHuman;

    

    public void SetTargetHuman(HumanController human)
    {
        _targetHuman = human;
        Absorb(_targetHuman);

    }

    void Absorb(HumanController human)
    {
        /* 
         
        Managers.ObjectManager.Despawn<HumanController>(human); 

        */
    }

    public void OnSpawn()
    {
        //Absorb(_targetHuman);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
