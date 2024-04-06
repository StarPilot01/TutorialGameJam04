using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using DG.Tweening;

public class HumanController : CreatureController
{
    
    



    [SerializeField]
    int _rewardLiverEnergy = 1;

    public int RewardLiverEnergy { get { return _rewardLiverEnergy; } }

    float _liverMalfunctionValue = 0.0f;
    int _liverMalfunctionLevel = 1;
    bool _absorbed = false;

    private void Awake()
    {
        Init();
    }


    public override bool Init()
    {
        base.Init();

        CreatureType = ECreatureType.Human;




        return true;
    }


    public void Start()
    {
       
    }


    public void Update()
    {

    }

    public void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
        transform.localScale = Vector3.one;
        Managers.AICommander.RequestCommand(this);
    }

    private void OnMouseDown()
    {
        Managers.GameManager.OnHumanClicked(this);
    }


    public void BeingAbsorbed()
    {
        Sequence sequence = DOTween.Sequence().SetAutoKill(false)
            .Append(transform.DOMoveY(transform.position.y + 1.5f, 1))
            .Join(_renderer.DOFade(0, 0.75f))
            .OnComplete(() =>
            {
                Debug.Log("Complete");
                Managers.ObjectManager.Despawn<HumanController>(this);
            }
            ); 

        //흡수당할 때 목적지에 도착으로 변경하고 정지
        

        _absorbed = true;
        PathFindingState = EPathfindingState.ArrivedDestination;

        //추가 애니메이션 실행

        transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
        _animator.Play("Fall");
        
    }
}
 