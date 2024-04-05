using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BuildingController : BaseController
{
    //������ 
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        ObjectType = EObjectType.Building;

        return true;
    }
}
