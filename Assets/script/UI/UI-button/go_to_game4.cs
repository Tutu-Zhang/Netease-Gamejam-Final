using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class go_to_game4 : MonoBehaviour
{
    public Button button;

    private void Start()//开始时执行
    {
        button.onClick.AddListener(GotoNew);//当名为button1的button被点击时，执行Gotonew函数,button1需要在unity中赋值
    }
    private void GotoNew()
    {
        AudioManager.Instance.PlayEffect("按钮");
        LevelManager.Instance.level = 4;

        if (PlayerPrefs.GetString("lv3Passed") == "yes")
        {
            SceneManager.LoadScene("BeforeGame");
        }
        else
        {
            GameObject obj = GameObject.Find("/Canvas/GameWindow/Tips");
            obj.SetActive(true);
        }
    }
}
