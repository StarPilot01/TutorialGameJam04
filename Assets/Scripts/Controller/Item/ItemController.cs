using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//모든 아이템은 먹을 수 있다고 상정
public class ItemController : BaseController , IEatable
{
    public EItemType ItemType { get; protected set; }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        ObjectType = EObjectType.Item;

        

        return true;
    }

    public virtual void OnEat()
    {
        
    }

    public virtual EItemType ReturnType()
    {
        return EItemType.None;
    }
}
