using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static bool s_init = false;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    GameManager _gameManager = new GameManager();

    ObjectManager _objectManager = new ObjectManager();
    
    ScoreManager _scoreManager = new ScoreManager();

    AICommander _aiCommander = new AICommander();

    public static GameManager GameManager { get { return Instance?._gameManager; } }
    public static ObjectManager ObjectManager { get { return Instance?._objectManager; } }
    public static ScoreManager ScoreManager { get { return Instance?._scoreManager; } }
    public static AICommander AICommander { get { return Instance?._aiCommander; } }

    #endregion

    #region Core
    UIManager _uiManager = new UIManager();
    SoundManager _soundManager = new SoundManager();
    SceneManagerEX _sceneManager = new SceneManagerEX();

    public static UIManager UIManager { get { return Instance?._uiManager; } }
    public static SoundManager SoundManager { get { return Instance?._soundManager; } }
    public static SceneManagerEX SceneManager { get { return Instance?._sceneManager; } }


    #endregion



    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject managersObject = GameObject.Find("@Managers");
            if(managersObject == null)
            {
                //Debug.LogError("Managers 오브젝트 없음");
                managersObject = new GameObject { name = "@Managers" };
                managersObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(managersObject);
            s_instance = managersObject.GetComponent<Managers>();

            //UIManager.Init();
            
            //ScoreManager.Test();


        }   
    }

    public static void Release()
    {

    }
}
