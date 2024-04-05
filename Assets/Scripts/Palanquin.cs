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
        
        이거 아래 함수 자체가 HumanController Absorbed 함수에서 실행하는게 나을거같아서 옮겼습니다.
        사실 Dotween에 연계하는게 편해서요 ㅎㅎ

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
