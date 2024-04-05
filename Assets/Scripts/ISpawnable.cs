using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable 
{
    //ObjectManager를 통해 스폰 되었을 떄 마지막에 수행하고 싶은 절차 넣으면 됨
    public void OnSpawn();
}
