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
        //���һ�ζ�û�н��д浵����ô��ʼ�����ݣ������Ƿ�浵���λyes
        if (PlayerPrefs.GetString("ifSaved","noSave") == "noSave")
        {
            SaveAndLoad_Init();
        }

        Register("beginButton").onClick = onStartGameBtn;

        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("LoginUI����BGM");
            AudioManager.Instance.PlayBGM("����BGM");
        }

    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {

        AudioManager.Instance.PlayEffect("��ť");
        Debug.Log("lv0: " + PlayerPrefs.GetString("lv0Passed"));

        if (PlayerPrefs.GetString("lv0Passed") == "no")
        {
            LevelManager.Instance.level = 0;

            SceneManager.LoadScene("BeforeGame");
        }
        else
        {
            //��ת���ؿ�ѡ�񳡾�
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
