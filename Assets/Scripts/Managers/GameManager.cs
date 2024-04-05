using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static Define;
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

    const float palanquinYDiff = 0.5f;
    
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

    void EatHuman(HumanController human)
    {
        //꽃가마 생성

        InstantiatePalanquin(human);

        //사운드 

        //간 에너지 갱신 , 구미호 얼굴 표정 변화
        Managers.ScoreManager.LiverEnergy += human.RewardLiverEnergy;


    }

    void InstantiatePalanquin(HumanController human)
    {
        Vector3 palanquinPos = new Vector3(human.transform.position.x, human.transform.position.y + palanquinYDiff, 0);
        Palanquin palanquin = Managers.ObjectManager.Spawn<Palanquin>(palanquinPos, "Palanquin");
        palanquin.SetTargetHuman(human);
    }
}
