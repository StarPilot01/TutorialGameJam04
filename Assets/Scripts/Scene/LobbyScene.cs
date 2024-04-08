using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyScene : BaseScene
{
    LobbyScene_UI _ui;

    private void Awake()
    {
        _ui = FindObjectOfType<LobbyScene_UI>();

    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.LobbyScene;



       
    }

    public override void Clear()
    {

    }
}
