using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//��ʼ����
public class SelectUI : UIBase
{
    private void Awake()
    {
        //if (AudioManager.Instance != null)
            if (!AudioManager.Instance.isPlayingBeginBGM)
            {
                Debug.Log("SelectUI����BGM");
                AudioManager.Instance.PlayBGM("����BGM");
            }
    }
}