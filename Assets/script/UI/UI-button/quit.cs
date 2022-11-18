using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quit : MonoBehaviour
{
    public Button button;
    private void Start()
    {/*查找按钮组件并添加事件(点击事件)*/
        button.onClick.AddListener(OnClick);
    }

    /*点击时触发*/
    private void OnClick()
    {
        AudioManager.Instance.PlayEffect("按钮");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
   
    }


}