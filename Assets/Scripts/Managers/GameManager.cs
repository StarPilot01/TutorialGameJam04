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
            
            
            OnClickModeChanged?.Invoke(_clickMode);

            
        }
    }

    float _elapsedTime;
    public float ElapsedTime { get { _elapsedTime += Time.deltaTime;  return _elapsedTime; } }

    Kumiho _kumiho;
    public Kumiho kumiho 
    { 
        get 
        {
            if(_kumiho == null)
            {
                GameScene gameScene = Managers.SceneManager.CurrentScene as GameScene;

                if (gameScene == null)
                {
                    Debug.Assert(false);
                }
                _kumiho = gameScene.Kumiho;
            }
            return _kumiho;
        } 
    }
    #region Action
    public event Action OnGameOver;
    public event Action OnEatHuman;

    public UnityAction<EClickMode> OnClickModeChanged;
    
    #endregion

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
            case EClickMode.Kill:
                Managers.ObjectManager.Instantiate("Blood Splash").transform.position = human.transform.position;
                human.Kill();
                break;
        }
    }

    public void OnItemClicked(ItemController item)
    {

        if(ClickMode != EClickMode.Pickup)
        {
            return;
        }


        switch(item.ItemType)
        {
            case EItemType.Soju:
                Managers.ObjectManager.Despawn<ItemController>(item);
                break;
            case EItemType.Ursa:
                Managers.ObjectManager.Despawn<ItemController>(item);
                break;
        }
    }
    

    void EatHuman(HumanController human)
    {
        
        kumiho.EatHuman(human);

        //꽃가마 생성
        Palanquin palanquin = InstantiatePalanquin(human);

        Sequence sequence = DOTween.Sequence()
            .Append(palanquin.transform.DOShakeRotation(0.4f, 5f))
            .Append(palanquin.GetComponent<SpriteRenderer>().DOFade(0, 0.4f))
            .OnComplete(() => Managers.ObjectManager.Despawn<Palanquin>(palanquin));


        //사운드 


        
    }

    Palanquin InstantiatePalanquin(HumanController human)
    {
        Vector3 palanquinPos = new Vector3(human.transform.position.x, human.transform.position.y + palanquinYDiff, 0);
        Palanquin palanquin = Managers.ObjectManager.Spawn<Palanquin>(palanquinPos, "Palanquin");
        palanquin.SetTargetHuman(human);

        return palanquin;
    }
}
