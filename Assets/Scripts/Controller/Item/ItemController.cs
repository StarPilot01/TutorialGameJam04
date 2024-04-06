using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//��� �������� ���� �� �ִٰ� ����
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
