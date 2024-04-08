using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type, Transform parents = null)
    {
        switch (CurrentScene.SceneType)
        {
            case Define.Scene.LobbyScene:
                SceneManager.LoadScene(GetSceneName(type));
                break;

            case Define.Scene.GameScene:
                SceneManager.LoadScene(GetSceneName(type));
                break;
            
        }

    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
}
