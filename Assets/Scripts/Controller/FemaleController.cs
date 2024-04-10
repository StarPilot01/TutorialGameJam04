using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class FemaleController : HumanController
{
    public override bool Init()
    {
        base.Init();

        Gender = EHumanGender.Female;


        return true;
    }
}
