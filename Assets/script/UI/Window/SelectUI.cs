using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//开始界面
public class SelectUI : UIBase
{
    private void Awake()
    {
        //if (AudioManager.Instance != null)
            if (!AudioManager.Instance.isPlayingBeginBGM)
            {
                Debug.Log("SelectUI播放BGM");
                AudioManager.Instance.PlayBGM("开场BGM");
            }
    }
}