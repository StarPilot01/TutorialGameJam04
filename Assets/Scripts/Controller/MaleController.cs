using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MaleController : HumanController
{
    public override bool Init()
    {
        base.Init();

        Gender = EHumanGender.Male;


        return true;
    }
}
