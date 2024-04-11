using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver_Popup : MonoBehaviour
{
    public void OnClickReturn()
    {
        Managers.GameManager.ResetAll();

        Managers.SceneManager.LoadScene(Define.Scene.LobbyScene);
    }
}
