using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static Define;
using DG.Tweening;
using UnityEngine.Events;

public class GameManager
{


    EClickMode _clickMode;
    public EClickMode ClickMode 
    { 
        get
        {
            return _clickMode;
        }
        set
        {
            _clickMode = value;

            //UI 갱신   
        }
    }

    public event Action OnGameOver;
    public event Action OnEatHuman;

    const float palanquinYDiff = 1.1f;
    
    public GameManager()
    {
        Init();
    }
    public void Init()
    {
        ClickMode = EClickMode.Eat;
    }

    public void GameOver()
    {
        //Action 호출
        OnGameOver();
        
    }
    
    public void OnHumanClicked(HumanController human)
    {
        switch(_clickMode)
        {
            case EClickMode.Eat:
                EatHuman(human);
                break;
            case EClickMode.Place:
                break;
        }
    }


    public UnityAction<string> KumihoAction;

    void EatHuman(HumanController human)
    {
        human.BeingAbsorbed();

        //꽃가마 생성
        Palanquin palanquin = InstantiatePalanquin(human);

        Sequence sequence = DOTween.Sequence().SetAutoKill(false)
            .Append(palanquin.transform.DOShakeRotation(1f, 5f))
            .Append(palanquin.GetComponent<SpriteRenderer>().DOFade(0, 1f))
            .OnComplete(() => Managers.ObjectManager.Despawn<Palanquin>(palanquin));


        //사운드 

        //간 에너지 갱신 , 구미호 얼굴 표정 변화

        Managers.ScoreManager.LiverEnergy += human.RewardLiverEnergy;
        Debug.Log(Managers.ScoreManager.LiverEnergy);

        //Succeed, Idle, Good, Fail, Bad
        KumihoAction.Invoke("Succeed");
    }

    Palanquin InstantiatePalanquin(HumanController human)
    {
        Vector3 palanquinPos = new Vector3(human.transform.position.x, human.transform.position.y + palanquinYDiff, 0);
        Palanquin palanquin = Managers.ObjectManager.Spawn<Palanquin>(palanquinPos, "Palanquin");
        palanquin.SetTargetHuman(human);

        return palanquin;
    }
}
