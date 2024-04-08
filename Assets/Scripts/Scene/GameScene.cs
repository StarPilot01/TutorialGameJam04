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
    }

        // Update is called once per frame
    void Update()
    {
        
    }

    public override void Clear()
    {
        
    }
}
