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

        Register("beginButton").onClick = onStartGameBtn;

        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("LoginUI播放BGM");
            AudioManager.Instance.PlayBGM("beginBGM-Changes-fiftysounds");
        }
    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        AudioManager.Instance.PlayEffect("按钮");

        //跳转到关卡选择场景
        SceneManager.LoadScene("selectPerfossionScene");
    }
}
