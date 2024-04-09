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


    int liverEnergy = 0;
    int maxLiverEnergy = 100; //임의로 최대치 정해놨습니다.


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
    public Action OnLiverEnergyChanged;
    public Action OnDead;
    #endregion
    public int LiverEnergy
    {
        get
        {
            return liverEnergy;
        }
        set
        {
            liverEnergy = value;

            if (liverEnergy > maxLiverEnergy)
                liverEnergy = maxLiverEnergy;


            OnLiverEnergyChanged.Invoke();


            PlayClickedAnim(value);
            //UpdateStateAnim();

            if (liverEnergy <= 0)
            {
                OnDead();
            }

        }
    }

    public int MaxLiverEnergy { get { return maxLiverEnergy; } set { maxLiverEnergy = value; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Managers.GameManager.ClickMode = EClickMode.Eat;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Managers.GameManager.ClickMode = EClickMode.Place;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Managers.GameManager.ClickMode = EClickMode.Pickup;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Managers.GameManager.ClickMode = EClickMode.Kill;
        }
    }

    IEnumerator CoPlayClickedAnimation(bool isSucceed , float duration)
    {
        if (isSucceed)
        {
            
            _animator.Play("Succeed",0,0);
        }
        else
        {
            _animator.Play("Fail",0,0);

        }

        yield return new WaitForSeconds(duration);

        UpdateStateAnim();
    }

    public void EatHuman(HumanController human)
    {
        human.BeingAbsorbed();
        LiverEnergy += human.RewardLiverEnergy;



    }

    

    void PlayClickedAnim(float value)
    {
        if (_coPlayClickedAnimation != null)
        {
            StopCoroutine(_coPlayClickedAnimation);
        }
        _coPlayClickedAnimation =
            StartCoroutine(CoPlayClickedAnimation(value >= 0, _clickedAnimationDuration));
    }
    void UpdateStateAnim()
    {
        string playAnim = EKumihoState.Bad.ToString();

        foreach(var state in _stateRanges)
        {
            if(liverEnergy >= state.Value)
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
    
}
