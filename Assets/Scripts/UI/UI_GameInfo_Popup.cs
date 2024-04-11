using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameInfo_Popup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClosed()
    {
        gameObject.SetActive(false);
    }
}
