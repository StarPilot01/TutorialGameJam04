using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using DG.Tweening;

public class HumanController : CreatureController
{
    
    



    [SerializeField]
    int _rewardLiverEnergy = 5;

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
        Managers.AICommander.RequestCommand(this);
    }

    private void OnMouseDown()
    {
        BeingAbsorbed();
        Managers.GameManager.OnHumanClicked(this);

    }


    public void BeingAbsorbed()
    {
        Sequence sequence = DOTween.Sequence().SetAutoKill(false)
            .Append(transform.DOMoveY(transform.position.y + 1.5f, 1))
            .Join(transform.GetComponent<SpriteRenderer>().DOFade(0, 0.75f))
            .OnComplete(() =>
            {
                Debug.Log("Complete");
                Managers.ObjectManager.Despawn<HumanController>(this);
            }
            ); 

        //������� �� �������� �������� �����ϰ� ����
        

        _absorbed = true;
        PathFindingState = EPathfindingState.ArrivedDestination;

        //�߰� �ִϸ��̼� ����
        //_animator.Play("Absorbed");
    }
}
 