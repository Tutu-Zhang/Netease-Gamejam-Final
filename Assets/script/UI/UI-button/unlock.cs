using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlock : MonoBehaviour
{

    public Button button;

    private void Start()//开始时执行
    {
        button.onClick.AddListener(Unlock);//当名为button1的button被点击时，执行Gotonew函数,button1需要在unity中赋值
    }
    private void Unlock()
    {
        PlayerPrefs.SetString("ifSaved", "yesSave");

        PlayerPrefs.SetString("lv0Passed", "yes");
        PlayerPrefs.SetString("lv1Passed", "yes");
        PlayerPrefs.SetString("lv2Passed", "yes");
        PlayerPrefs.SetString("lv3Passed", "yes");
        PlayerPrefs.SetString("lv4Passed", "yes");

        PlayerPrefs.Save();

        //Debug.Log(PlayerPrefs.GetString("ifSaved"));
        //Debug.Log("lv0: " + PlayerPrefs.GetString("lv0Passed"));


    }
}
