using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Soju : ItemController
{
    [SerializeField]
    int _liverMalfunctionValueIncrement;
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        ItemType = EItemType.Soju;
        return true;
    }

    public override void OnEat(HumanController human)
    {
        
        Managers.ObjectManager.Despawn<Soju>(this);

        human.LiverMalfunctionValue += _liverMalfunctionValueIncrement;

    }

    public void OnMouseDown()
    {
        Managers.GameManager.OnItemClicked(this);
    }
}
