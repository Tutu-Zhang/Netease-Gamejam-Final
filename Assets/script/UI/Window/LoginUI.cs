using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//开始界面
public class LoginUI : UIBase
{
    private void Start()
    {
        //如果一次都没有进行存档，那么初始化数据，并将是否存档标记位yes
        if (PlayerPrefs.GetString("ifSaved","noSave") == "noSave")
        {
            SaveAndLoad_Init();
        }

        Register("beginButton").onClick = onStartGameBtn;

        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("LoginUI播放BGM");
            AudioManager.Instance.PlayBGM("开场BGM");
        }

    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {

        AudioManager.Instance.PlayEffect("按钮");
        Debug.Log("lv0: " + PlayerPrefs.GetString("lv0Passed"));

        if (PlayerPrefs.GetString("lv0Passed") == "no")
        {
            LevelManager.Instance.level = 0;

            SceneManager.LoadScene("BeforeGame");
        }
        else
        {
            //跳转到关卡选择场景
            SceneManager.LoadScene("selectScene");
        }

    }

    private void SaveAndLoad_Init()
    {
        PlayerPrefs.SetString("ifSaved", "yes");

        PlayerPrefs.SetString("lv0Passed", "no");
        PlayerPrefs.SetString("lv1Passed", "no");
        PlayerPrefs.SetString("lv2Passed", "no");
        PlayerPrefs.SetString("lv3Passed", "no");
        PlayerPrefs.SetString("lv4Passed", "no");

        PlayerPrefs.Save();


    }
}
