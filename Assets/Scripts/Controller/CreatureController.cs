using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : BaseController
{
    public ECreatureType CreatureType { get; protected set; }

    protected enum EPathfindingState
    { 
        Moving,
        FindWayPoint,
        ArrivedDestination,
        Wait,
    
    }


    protected bool _bStopAllAction = false;
    protected Vector2 _prevCellPos;

    protected Rigidbody2D _rigidBody;
    protected bool _bMove = false;
    protected bool _bArrived = true;
    protected Animator _animator;
    protected SpriteRenderer _renderer;

    [SerializeField]
    protected float _moveSpeed;
    protected float _adjustmentY = -0.375f;

    protected float _animMoveSpeed;
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            _moveSpeed = value;
            
        }
    }

    protected Vector2 _destinationCellPos;
    protected Vector2 _wayPointCellPos;
    protected Vector2 _moveDirVec;

    protected EMoveDir _moveDir = EMoveDir.Down;



    protected EMoveDir MoveDir
    {
        get
        {
            return _moveDir;
        }
        set
        {
            _moveDir = value;

            moveAnimUpdate(_moveDir);

        }
    }

    protected EPathfindingState _pathFindingState;

    protected EPathfindingState PathFindingState
    {
        get
        {
            return _pathFindingState;
        }
        set
        {
            _pathFindingState = value;

            switch(_pathFindingState)
            {
                case EPathfindingState.Moving:
                    
                    break;
                case EPathfindingState.FindWayPoint:
                    FindWayPoint();
                    break;
                case EPathfindingState.ArrivedDestination:
                    MoveDir = EMoveDir.None;
                    ArrivedAtDestination();
                    break;
                case EPathfindingState.Wait:
                    {
                        MoveDir = EMoveDir.None;
                        _moveDirVec = Vector2.zero;
                        _rigidBody.velocity = Vector2.zero;
                    }
                    

                    break;

                default:
                    break;
            }
        }
    }


    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        ObjectType = EObjectType.Creature;

        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        MoveSpeed = _moveSpeed;


        return true;
    }

    

    protected void FixedUpdate()
    {
        Move();
    }
    protected void LateUpdate()
    {
        AnimUpdate();
    }



    protected virtual void FindWayPoint()
    {
        Vector2 diff = _destinationCellPos - _cellPos;

        _wayPointCellPos = _cellPos;


        //TODO : 알고리즘 수정예정 현재는 x좌표 먼저 이동하고 y좌표로 이동하게 만들었음

        if (diff.x != 0)
        {
            if (diff.x > 0)
            {
                _wayPointCellPos.x = _cellPos.x + 1;

                MoveDir = EMoveDir.Right;
            }
            else
            {
                _wayPointCellPos.x = _cellPos.x - 1;
                MoveDir = EMoveDir.Left;

            }
        }
        else if (diff.y != 0)
        {
            if (diff.y > 0)
            {
                _wayPointCellPos.y = _cellPos.y + 1;


                MoveDir = EMoveDir.Down;

            }
            else
            {
                _wayPointCellPos.y = _cellPos.y - 1;
                MoveDir = EMoveDir.Up;

            }
        }

        //Debug.Log(_wayPointCellPos);

        PathFindingState = EPathfindingState.Moving;
    }

    protected virtual void Move()
    {
        if(_bStopAllAction)
        {
            return;
        }


        if (PathFindingState == EPathfindingState.Moving)
        {



            if(HasArrivedAtWaypoint())
            {
                if(_cellPos == _destinationCellPos)
                {
                    PathFindingState = EPathfindingState.ArrivedDestination;
                }
                else
                {
                    PathFindingState = EPathfindingState.FindWayPoint;
                }
            }
            else
            {
                _rigidBody.velocity = _moveDirVec * MoveSpeed * Time.fixedDeltaTime;

            }




        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
        }

    }


    protected void RefreshCellPos()
    {
        //_prevCellPos = _cellPos;
        _cellPos = GameMap.WorldToCell(_rigidBody.position.x, _rigidBody.position.y);
        
        //현재 Pos랑 진행방향 Pos에 우루사 못 놓게 하기
    }

    protected virtual void ArrivedAtDestination()
    {
        

    }

    bool HasArrivedAtWaypoint()
    {
        Vector2 myPos = new Vector2(_rigidBody.position.x, _rigidBody.position.y);

        Vector2 wayPointPos = GameMap.CellToWorld(_wayPointCellPos);
        Vector2 diff = myPos - wayPointPos;

        const float EPSILON = 0.055f;

        if (Mathf.Abs(diff.magnitude) <= EPSILON)
        {
            _rigidBody.MovePosition(GameMap.CellToWorld(_wayPointCellPos));


            RefreshCellPos();

            MoveDir = EMoveDir.None;


            return true;
        }

        return false;
    }


    public void MoveToDest(Vector2 pos)
    {
        
        _destinationCellPos = pos;

        PathFindingState = EPathfindingState.FindWayPoint;


    }

    public virtual void Wait(float time)
    {
        StartCoroutine(CoWait(time));

    }

    protected virtual IEnumerator CoWait(float time)
    {
        
        PathFindingState = EPathfindingState.Wait;

        yield return new WaitForSeconds(time);



    }


    public override void OnSpawn()
    {
        base.OnSpawn();

        RefreshCellPos();

    }




    void AnimUpdate()
    {
        _animator.SetFloat("MoveSpeed", _animMoveSpeed);
    }

    protected virtual void moveAnimUpdate(EMoveDir dir)
    {
        
    }
}
