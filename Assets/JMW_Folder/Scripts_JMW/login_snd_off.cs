using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class login_snd_off : MonoBehaviour
{
    public GameObject login_snd;
    

    void Start()
    {
        LoginSndOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoginSndOff()
    {
        login_snd.gameObject.SetActive(false);
    }
}
