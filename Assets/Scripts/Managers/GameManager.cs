using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static Define;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.AI;

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
            
            Managers.SoundManager.Play(ESoundType.SFX, "SwitchMode");

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
    public event Action<int> OnUrsaChagned;

    public event Action<EClickMode> OnClickModeChanged;
    
    #endregion

    const float palanquinYDiff = 1.1f;
    public bool bGameOver { get; private set; }
    int _ursaCount = 0;
    int _pickedupSojuCount = 0;


    public bool FirstPlay { get; set; } = true;

    protected int UrsaCount
    {
        get { return _ursaCount; }
        set
        {
            _ursaCount = value;
            OnUrsaChagned?.Invoke(_ursaCount);
        }
    }

    public GameManager()
    {
        Init();
    }
    public void Init()
    {
        _clickMode = EClickMode.Eat;
    }

    public void ResetAll()
    {
        _elapsedTime = 0.0f;
        ClickMode = EClickMode.Eat;
        UrsaCount = 0;
        _pickedupSojuCount = 0;
        bGameOver = false;

        _kumiho.ResetAll();
        Managers.AICommander.ResetAll();
        GameScene gameScene = (GameScene)Managers.SceneManager.CurrentScene;
        gameScene.ResetAll();
    }

    public void GameOver()
    {
        //Action 호출
        OnGameOver?.Invoke();
        bGameOver = true;
        Managers.SoundManager.Stop(ESoundType.BGM);
        Managers.SoundManager.Play(ESoundType.SFX, "GameOver");

        Managers.AICommander.ResetAll();


        //ResetAll();
        //Managers.AICommander.StopAllHuman();
    }

    public void OnHumanClicked(HumanController human)
    {
        switch(_clickMode)
        {
            case EClickMode.Eat:
                Managers.SoundManager.Play(ESoundType.SFX, "Eat");
                EatHuman(human);
                break;
            case EClickMode.Kill:
                Managers.SoundManager.Play(ESoundType.SFX, "Kill");
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

        Managers.ObjectManager.Instantiate("Pickup_Effect").transform.position = item.transform.position;

        switch (item.ItemType)
        {
            case EItemType.Soju:
                {
                    _pickedupSojuCount++;
                    CheckExchangeableSojuToUrsa();
                }

                Managers.SoundManager.Play(ESoundType.SFX, "Pickup");
                Managers.ObjectManager.Despawn<ItemController>(item);
                break;
            case EItemType.Ursa:
                Managers.SoundManager.Play(ESoundType.SFX, "Pickup");

                Managers.ObjectManager.Despawn<ItemController>(item);
                break;
        }
    }

    public void RequestInstantiateUrsa(Vector2 pos)
    {
        if(ClickMode != EClickMode.Place || !GameMap.IsInsideMapArea(pos) || _ursaCount <= 0)
        {
            return;
        }


        Managers.SoundManager.Play(ESoundType.SFX, "Place");

        GameObject ursa = Managers.ObjectManager.Instantiate("Ursa");
        ursa.transform.position = pos;

        UrsaCount--;
    }
    
    void CheckExchangeableSojuToUrsa()
    {
       

        if (_pickedupSojuCount >= 3)
        {
            UrsaCount++;
            _pickedupSojuCount = 0;
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
