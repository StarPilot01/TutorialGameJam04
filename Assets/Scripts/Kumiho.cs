using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;
using static UnityEngine.Rendering.DebugUI;

public class Kumiho : MonoBehaviour
{
    Animator _animator;

    bool _bDead = false;
    int _liverEnergy = 40;
    int _maxLiverEnergy = 100;

    int _liverEnergyDecrement = 1;

    enum EKumihoState
    {
        Bad,
        Default,
        Good
    }

    Dictionary<EKumihoState, int> _stateRanges = new Dictionary<EKumihoState, int>()
    {
        { EKumihoState.Bad , 20},
        { EKumihoState.Default , 50},
        { EKumihoState.Good , 70}
    };


    Coroutine _coPlayClickedAnimation;

    [SerializeField]
    float _clickedAnimationDuration;

    #region Action
    public Action<int> OnLiverEnergyChanged;
    public Action OnDead;
    #endregion
    public int LiverEnergy
    {
        get
        {
            return _liverEnergy;
        }
        set
        {
            _liverEnergy = value;

            if (_liverEnergy > _maxLiverEnergy)
                _liverEnergy = _maxLiverEnergy;


            OnLiverEnergyChanged.Invoke(_liverEnergy);


            //PlayClickedAnim(value);
            UpdateStateAnim();
            if (_liverEnergy <= 0)
            {
                _liverEnergy = 0;
                _bDead = true;
                OnDead?.Invoke();
            }

        }
    }

    public int MaxLiverEnergy { get { return _maxLiverEnergy; } set { _maxLiverEnergy = value; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        LiverEnergy = 40;

        StartCoroutine(CoDecreaseLiverEnergyPredically(1.5f));

        OnDead += Dead;
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
    }

    IEnumerator CoDecreaseLiverEnergyPredically(float cycle)
    {
        while(!_bDead)
        {
            _liverEnergy -= _liverEnergyDecrement;
            OnLiverEnergyChanged?.Invoke(_liverEnergy);

            yield return new WaitForSeconds(cycle);
        }
    }

    //IEnumerator CoPlayClickedAnimation(bool isSucceed , float duration)
    //{
    //    if (isSucceed)
    //    {
    //        
    //        _animator.Play("Succeed",0,0);
    //    }
    //    else
    //    {
    //        _animator.Play("Fail",0,0);
    //
    //    }
    //
    //    yield return new WaitForSeconds(duration);
    //
    //    UpdateStateAnim();
    //}

    public void EatHuman(HumanController human)
    {
        human.BeingAbsorbed();
        //LiverEnergy += human.RewardLiverEnergy;



    }

    void InputKey()
    {
        if(_bDead)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Managers.GameManager.ClickMode = EClickMode.Eat;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Managers.GameManager.ClickMode = EClickMode.Place;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Managers.GameManager.ClickMode = EClickMode.Pickup;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Managers.GameManager.ClickMode = EClickMode.Kill;
        }
    }
    void Dead()
    {
        _animator.Play("Fail");

        Managers.GameManager.GameOver();
    }

    //void PlayClickedAnim(float value)
    //{
    //    if (_coPlayClickedAnimation != null)
    //    {
    //        StopCoroutine(_coPlayClickedAnimation);
    //    }
    //    _coPlayClickedAnimation =
    //        StartCoroutine(CoPlayClickedAnimation(value >= 0, _clickedAnimationDuration));
    //}
    void UpdateStateAnim()
    {
        string playAnim = EKumihoState.Bad.ToString();

        foreach(var state in _stateRanges)
        {
            if(_liverEnergy >= state.Value)
            {
                playAnim = state.Key.ToString();
                
            }
            else
            {
                break;
            }
        }

        _animator.Play(playAnim);
    }


    public void ResetAll()
    {
        LiverEnergy = 40;
        _bDead = false;
        StopAllCoroutines();
    }
}
