using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Kumiho : MonoBehaviour
{
    [SerializeField] Animator anim;   

    // Start is called before the first frame update
    void Start()
    {
        Managers.GameManager.KumihoAction += SetAnim;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Managers.GameManager.ClickMode = EClickMode.Eat;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Managers.GameManager.ClickMode = EClickMode.Place;
        }
    }

    public void SetAnim(string str)
    {
        anim.Play(str);
    }
}
