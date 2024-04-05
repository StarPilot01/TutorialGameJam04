using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{

    public enum EObjectType
    { 
        Creature,
        Item,
        Building

    }

    public enum ECreatureType
    { 
        Human,
        Animal

    }

    public enum EItemType
    { 
        Soju,
        Ursa
    
    }



    public enum ESoundType
    {
        BGM,
        SubBGM,
        SFX,
        Max
    }

    public enum EMoveDir
    { 
        Up,
        Down,
        Right,
        Left,
        None
            
    }

    public enum EClickMode
    {
        Eat,
        Place
    }
}
