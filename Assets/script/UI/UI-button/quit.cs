using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quit : MonoBehaviour
{
    public Button button;
    private void Start()
    {/*���Ұ�ť���������¼�(����¼�)*/
        button.onClick.AddListener(OnClick);
    }

    /*���ʱ����*/
    private void OnClick()
    {
        AudioManager.Instance.PlayEffect("��ť");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
   
    }


}