using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlock : MonoBehaviour
{

    public Button button;

    private void Start()//��ʼʱִ��
    {
        button.onClick.AddListener(Unlock);//����Ϊbutton1��button�����ʱ��ִ��Gotonew����,button1��Ҫ��unity�и�ֵ
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
