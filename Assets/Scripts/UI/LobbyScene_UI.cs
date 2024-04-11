using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene_UI : MonoBehaviour
{
    [SerializeField]
    Button _startButton;
    [SerializeField]
    Button _exitButton;

    [SerializeField]
    GameObject _uiLobbyPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        Managers.SceneManager.LoadScene(Define.Scene.GameScene);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
        


    }

    public void Show_LobbySettingPopup()
    {
        _uiLobbyPopup.gameObject.SetActive(true);
    }
}
