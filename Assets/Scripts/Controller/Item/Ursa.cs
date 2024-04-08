using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Ursa : ItemController
{
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

    public override void OnEat()
    {
        Debug.Log("Eat Ursa");
        Managers.ObjectManager.Despawn<Ursa>(this);
    }

    public void OnMouseDown()
    {
        Managers.GameManager.OnItemClicked(this);

    }

}
