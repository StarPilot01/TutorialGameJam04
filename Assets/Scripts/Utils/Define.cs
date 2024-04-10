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

    public enum EHumanLiverState
    {
        Good,
        Common,
        Bad,
    }

    public enum EHumanGender
    { 
        Male,
        Female
    
    }


    public enum EItemType
    { 
        Soju,
        Ursa,
        
        None = 99,
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
    public enum Scene
    {
        
        
        LobbyScene,
        GameScene
    }
    public enum EClickMode
    {
        Eat,
        Place,
        Pickup,
        Kill
    }
}
