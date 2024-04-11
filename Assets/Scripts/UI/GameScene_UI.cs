using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class GameScene_UI : MonoBehaviour
{
    [SerializeField] 
    Image _liverFillImage;
    [SerializeField] 
    TextMeshProUGUI _elapsedTimeText;
    //[SerializeField] 
    //Image _gameModeIndicatorImage;
    [SerializeField] 
    Image[] _clickModeIndicatorImages;
    [SerializeField]
    TextMeshProUGUI _possessionCountText;
    [SerializeField]
    TextMeshProUGUI _liverEnergyText;

    [SerializeField]
    GameObject _gameInfoPopup;
    [SerializeField]
    GameObject _gameOverPopup;

    [HideInInspector]
    public bool ShowingGameInfoPopup;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        
        Managers.GameManager.kumiho.OnLiverEnergyChanged += SetLiverEnergyFill;
        Managers.GameManager.OnClickModeChanged += VisibleIndicator;
        Managers.GameManager.OnUrsaChagned += UpdateUrsaCount;
        Managers.GameManager.kumiho.OnLiverEnergyChanged += UpdateLiverEnergy;


        if(Managers.GameManager.FirstPlay)
        {
            _gameInfoPopup.SetActive(true);
            Managers.GameManager.FirstPlay = false;
            ShowingGameInfoPopup = true;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!ShowingGameInfoPopup && !Managers.GameManager.bGameOver)
        {
           SetElapsedTime(Managers.GameManager.ElapsedTime);

        }
    }


    public void SetLiverEnergyFill(int energy)
    {
        _liverFillImage.fillAmount =
            (float)Managers.GameManager.kumiho.LiverEnergy / (float)Managers.GameManager.kumiho.MaxLiverEnergy;
    }

    public void SetElapsedTime(float time)
    {
        _elapsedTimeText.text = ((int)time / 60).ToString("D2") + ":" + ((int)time % 60).ToString("D2");
    }

    public void VisibleIndicator(EClickMode clickMode)
    {
        for(int i = 0;  i < _clickModeIndicatorImages.Length; i++)
        {
            _clickModeIndicatorImages[i].gameObject.SetActive(false);
        }

        _clickModeIndicatorImages[(int)clickMode].gameObject.SetActive(true);

    }

    //public void SetClickModeIndicatorVisible(int index)
    //{
    //    modeImage.sprite = modeSprites[index];
    //}

    public void UpdateUrsaCount(int count)
    {
        _possessionCountText.text = count.ToString();
    }

    public void UpdateLiverEnergy(int energy)
    {
        _liverEnergyText.text = energy.ToString();  
    }

    public void ReturnToLobby()
    {

        Managers.GameManager.ResetAll();

        Managers.SceneManager.LoadScene(Scene.LobbyScene);
    }

    
    public void ShowGameOverPopup()
    {
        _gameOverPopup.SetActive(true);

    }
    

    public void ResetAll()
    {
        Managers.GameManager.kumiho.OnLiverEnergyChanged -= SetLiverEnergyFill;
        Managers.GameManager.OnClickModeChanged -= VisibleIndicator;
        Managers.GameManager.OnUrsaChagned -= UpdateUrsaCount;
        Managers.GameManager.kumiho.OnLiverEnergyChanged -= UpdateLiverEnergy;
    }
}
