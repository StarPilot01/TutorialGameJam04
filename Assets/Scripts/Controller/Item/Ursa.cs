using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Ursa : ItemController
{
    [SerializeField]
    int _liverMalfunctionValueDecrement;
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        ItemType = EItemType.Ursa;
        return true;
    }

    public override void OnEat(HumanController human)
    {
        
        Managers.ObjectManager.Despawn<Ursa>(this);

        human.LiverMalfunctionValue -= _liverMalfunctionValueDecrement;
    }

    public void OnMouseDown()
    {
        Managers.GameManager.OnItemClicked(this);

    }

}
