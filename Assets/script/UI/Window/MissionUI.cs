using System;
using System.Collections.Specialized;//字典列表
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MissionUI : MonoBehaviour
{

    public Button GoToNext;

    // Start is called before the first frame update
    void Start()
    {
        GoToNext.onClick.AddListener(gotoNext);
    }

    public void gotoNext()
    {
        SceneManager.LoadScene("winSelect");//直接进入选宝界面。调试用
    }
}
