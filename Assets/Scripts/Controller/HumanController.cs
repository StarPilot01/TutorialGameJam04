using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

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
        Managers.GameManager.OnHumanClicked(this);
    }


    public void BeingAbsorbed()
    {
        //������� �� �������� �������� �����ϰ� ����
        _absorbed = true;
        PathFindingState = EPathfindingState.ArrivedDestination;

        //�߰� �ִϸ��̼� ����
    }




}
