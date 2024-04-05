using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour , ISpawnable
{
    public EObjectType ObjectType { get; protected set; }

    bool _init = false;



    protected Vector2 _cellPos;

    public Vector2 CellPos { get { return _cellPos; } set { _cellPos = value; } }


    void Awake()
    {
        Init();
    }

    public virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }

    public virtual void OnSpawn()
    {
        
    }
}
