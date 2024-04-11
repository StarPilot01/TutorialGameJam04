using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameScene : BaseScene
{
    GameScene_UI _ui;

    public GameScene_UI UI { get { return _ui; } }
    public Kumiho Kumiho { get; set; }


    //Kumiho _kumiho;
    SojuSpawner _sojuSpawner;
    HumanSpawner _humanSpawner;


    [SerializeField]
    float _sojuSpawnCycle;
    [SerializeField]
    float _humanSpawnCycle;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Managers.SoundManager.Play(ESoundType.BGM, "Game");

        Managers.GameManager.OnGameOver += StopSpawn;

        _sojuSpawner.SetSpawnCycle(_sojuSpawnCycle);
        _sojuSpawner.StartSpawn();

        _humanSpawner.SetSpawnCycle(_humanSpawnCycle);
        _humanSpawner.StartSpawn();

    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene;



        _ui = FindObjectOfType<GameScene_UI>();
        Kumiho = FindObjectOfType<Kumiho>();
        _sojuSpawner = new SojuSpawner();
        _humanSpawner = new HumanSpawner();


        Screen.SetResolution(1280, 720, false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Managers.GameManager.RequestInstantiateUrsa(pos);
        }
    }

    public override void Clear()
    {
        
    }

    void StopSpawn()
    {
        _sojuSpawner.Stop();
        _humanSpawner.Stop();
    }

    public void ResetAll()
    {
        Managers.GameManager.OnGameOver -= StopSpawn;
        StopSpawn();
        UI.ResetAll();

    }
}
