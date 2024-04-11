using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using DG.Tweening;

public class HumanController : CreatureController
{
    //AnimatorController
    //random으로 
    EHumanLiverState _liverState;

    public EHumanGender Gender { get; protected set; }


    public int RewardLiverEnergy { get { return _rewardLiverEnergyDic[_liverState]; } }



    Dictionary<EHumanLiverState, int> _stateRanges = new Dictionary<EHumanLiverState, int>()
    {
        { EHumanLiverState.Good , 30},
        { EHumanLiverState.Common , 45},
        { EHumanLiverState.Bad , 65}
    };
    
    Dictionary<EHumanLiverState, int> _rewardLiverEnergyDic = new Dictionary<EHumanLiverState, int>()
    {
        { EHumanLiverState.Good , 3},
        { EHumanLiverState.Common , -5},
        { EHumanLiverState.Bad , -10}
    };


    
    float _liverMalfunctionIncreasingCycleSec = 1;
    int _liverMalfunctionIncrement = 15;

    int _deadThreshold = 80;

    int _liverMalfunctionValue = 0;

    public int LiverMalfunctionValue
    {
        get
        {
            return _liverMalfunctionValue;
        }
        set
        {

            _liverMalfunctionValue = value;

            if(_liverMalfunctionValue < 0)
            {
                _liverMalfunctionValue = 0;
            }


            UpdateLiverState(_liverMalfunctionValue);
            CheckLiverMalfunctionValueThresholdExceeded();
            moveAnimUpdate(_moveDir);

        }
    }


    //int _liverMalfunctionLevel = 1;
    bool _bAbsorbed = false;
    bool _bDead = false;


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
        if (!_bAbsorbed && !_bDead)
        {
            Managers.GameManager.OnHumanClicked(this);

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(_bStopAllAction)
        {
            return;
        }


        IEatable eatable = collision.GetComponent<IEatable>();

        if (eatable != null && (!_bAbsorbed && !_bDead))
        {

            eatable.OnEat(this);

            
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
                _renderer.flipX = false;
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
                _renderer.flipX = false;
                _animMoveSpeed = _moveSpeed / 100f;

                break;
           

        }
    }


    void CheckLiverMalfunctionValueThresholdExceeded()
    {
        if(_liverMalfunctionValue >= _deadThreshold)
        {
            DeadFromLiverMalfunction();
        }
    }

    void DeadFromLiverMalfunction()
    {
        Managers.SoundManager.Play(ESoundType.SFX, "Dead");
        Managers.AICommander.NotifyDead(this);

        _bDead = true;
        //간 에너지 깎기
        Managers.GameManager.kumiho.LiverEnergy += RewardLiverEnergy;
        //죽음 애니메이션


        Sequence sequence = DOTween.Sequence()
            .Append(transform.GetChild(0).transform.DOMoveY(transform.GetChild(0).position.y + 1.5f, 0.7f))
            .Join(_renderer.DOFade(0, 0.50f))
            .OnComplete(() =>
            {
                Managers.ObjectManager.Despawn<HumanController>(this);

            }
            );

        _animator.Play("Fall");


    }

    void DeadFromAbsorption()
    {
        Managers.AICommander.NotifyDead(this);

        _bDead = true;
        //간 에너지 깎기
        //죽음 애니메이션

        Managers.ObjectManager.Despawn<HumanController>(this);
    }



    public void Kill()
    {
        Managers.AICommander.NotifyDead(this);

        _bDead = true;

        Managers.ObjectManager.Despawn<HumanController>(this);


    }

    public void BeingAbsorbed()
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.GetChild(0).transform.DOMoveY(transform.GetChild(0).position.y + 1.5f, 1))
            .Join(_renderer.DOFade(0, 0.75f))
            .OnComplete(() =>
            {
                DeadFromAbsorption();
            }
            ); 

    

        _bAbsorbed = true;
        Managers.GameManager.kumiho.LiverEnergy += RewardLiverEnergy;


        PathFindingState = EPathfindingState.Wait;

        
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
        while(!_bAbsorbed)
        {
            LiverMalfunctionValue += _liverMalfunctionIncrement;

            yield return new WaitForSeconds(cycle);
        }



    }


    public override void Wait(float time)
    {
        base.Wait(time);


    }


    protected override IEnumerator CoWait(float time)
    {
        //Debug.Log("Wait");
        PathFindingState = EPathfindingState.Wait;

        yield return new WaitForSeconds(time);


        Managers.AICommander.RequestCommand(this);

    }
    protected override void ArrivedAtDestination()
    {
        base.ArrivedAtDestination();

        if(_bStopAllAction)
        {
            Managers.AICommander.RequestCommand(this);

        }
    }

    public void StopAllAction()
    {
        _bStopAllAction = true;
        _animMoveSpeed = 0;
        MoveDir = EMoveDir.None;
        _moveDirVec = Vector2.zero;
        _rigidBody.velocity = Vector2.zero;
        PathFindingState = EPathfindingState.Wait;

        //StopAllCoroutines();
    }
}
 