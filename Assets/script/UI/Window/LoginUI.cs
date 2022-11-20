using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//��ʼ����
public class LoginUI : UIBase
{
    private void Start()
    {

        Register("beginButton").onClick = onStartGameBtn;

        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("LoginUI����BGM");
            AudioManager.Instance.PlayBGM("beginBGM-Changes-fiftysounds");
        }
    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        AudioManager.Instance.PlayEffect("��ť");

        //��ת���ؿ�ѡ�񳡾�
        SceneManager.LoadScene("selectPerfossionScene");
    }
}
