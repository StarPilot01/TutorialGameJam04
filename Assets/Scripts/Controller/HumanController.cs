using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using DG.Tweening;

public class HumanController : CreatureController
{



    //random으로 
    EHumanLiverState _liverState;

    [SerializeField]
    int _rewardLiverEnergy = 1;

    public int RewardLiverEnergy { get { return _rewardLiverEnergy; } }



    Dictionary<EHumanLiverState, int> _stateRanges = new Dictionary<EHumanLiverState, int>()
    {
        { EHumanLiverState.Good , 20},
        { EHumanLiverState.Common , 50},
        { EHumanLiverState.Bad , 70}
    };


    
    float _liverMalfunctionIncreasingCycleSec = 2;
    int _liverMalfunctionIncrement = 30;

    int _liverMalfunctionValue = 0;

    int LiverMalfunctionValue
    {
        get
        {
            return _liverMalfunctionValue;
        }
        set
        {
            _liverMalfunctionValue = value;


            UpdateLiverState(_liverMalfunctionValue);
            moveAnimUpdate(_moveDir);

        }
    }


    //int _liverMalfunctionLevel = 1;
    bool _absorbed = false;

    EMoveDir _prevDir;

    private void Awake()
    {
        Init();
    }


    public override bool Init()
    {
        base.Init();

        CreatureType = ECreatureType.Human;

        LiverMalfunctionValue = 0;


        return true;
    }


    public void Start()
    {
        StartCoroutine(IncreaseLiverMalfunctionValuePreodically(_liverMalfunctionIncreasingCycleSec));
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
        if (!_absorbed)
        {
            Managers.GameManager.OnHumanClicked(this);

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Eatable"))
        {
            IEatable eatable = collision.GetComponent<IEatable>();
            eatable.OnEat();

            
        }
    }

    protected override void moveAnimUpdate(EMoveDir dir)
    {
        string prefix = _liverState.ToString();

        if(dir != EMoveDir.None)
        {
            _prevDir = _moveDir;
        }

        switch (_moveDir)
        {
            case EMoveDir.Up:
                _moveDirVec = Vector2.up;
                _animator.Play(prefix + "_Upwalk");
                _animMoveSpeed = _moveSpeed / 100f;
                break;
            case EMoveDir.Down:
                _moveDirVec = Vector2.down;
                _animator.Play(prefix + "_Downwalk");
                _animMoveSpeed = _moveSpeed / 100f;
                break;
            case EMoveDir.Right:
                _moveDirVec = Vector2.right;
                _animator.Play(prefix + "_Leftwalk");
                _renderer.flipX = true;
                _animMoveSpeed = _moveSpeed / 100f;

                break;
            case EMoveDir.Left:
                _moveDirVec = Vector2.left;
                _animator.Play(prefix +"_Leftwalk");
                _animMoveSpeed = _moveSpeed / 100f;

                break;
            case EMoveDir.None:
                UpdateAnimOnStop(_prevDir);
                _moveDirVec = Vector2.zero;
                _animMoveSpeed = 0;
                break;

        }
    }

    void UpdateAnimOnStop(EMoveDir dir)
    {
        string prefix = _liverState.ToString();

        //EMoveDir prevDir = dir;
        switch (dir)
        {
            case EMoveDir.Up:
               
                _animator.Play(prefix + "_Upwalk");
                _animMoveSpeed = _moveSpeed / 100f;
                break;
            case EMoveDir.Down:
                
                _animator.Play(prefix + "_Downwalk");
                _animMoveSpeed = _moveSpeed / 100f;
                break;
            case EMoveDir.Right:
                
                _animator.Play(prefix + "_Leftwalk");
                _renderer.flipX = true;
                _animMoveSpeed = _moveSpeed / 100f;

                break;
            case EMoveDir.Left:
                
                _animator.Play(prefix + "_Leftwalk");
                _animMoveSpeed = _moveSpeed / 100f;

                break;
           

        }
    }



    public void BeingAbsorbed()
    {
        Sequence sequence = DOTween.Sequence().SetAutoKill(false)
            .Append(transform.GetChild(0).transform.DOMoveY(transform.GetChild(0).position.y + 1.5f, 1))
            .Join(_renderer.DOFade(0, 0.75f))
            .OnComplete(() =>
            {
                //Debug.Log("Complete");
                Managers.ObjectManager.Despawn<HumanController>(this);
            }
            ); 

        //흡수당할 때 목적지에 도착으로 변경하고 정지
        

        _absorbed = true;
        PathFindingState = EPathfindingState.ArrivedDestination;

        //추가 애니메이션 실행

        transform.GetChild(0).transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
        _animator.Play("Fall");
        
    }

    void UpdateLiverState(int liverValue)
    {
        EHumanLiverState state = EHumanLiverState.Good;
        

        foreach (var element in _stateRanges)
        {
            if (liverValue >= element.Value)
            {
                state = element.Key;

            }
            else
            {
                break;
            }
        }

        _liverState = state;


        
    }


    IEnumerator IncreaseLiverMalfunctionValuePreodically(float cycle)
    {
        while(!_absorbed)
        {
            LiverMalfunctionValue += _liverMalfunctionIncrement;

            yield return new WaitForSeconds(cycle);
        }



    }
}
 