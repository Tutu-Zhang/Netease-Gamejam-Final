using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class go_to_game2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    private void Start()//开始时执行
    {
        button.onClick.AddListener(GotoNew);//当名为button1的button被点击时，执行Gotonew函数,button1需要在unity中赋值
    }
    private void GotoNew()
    {
        AudioManager.Instance.PlayEffect("按钮");
        LevelManager.Instance.level = 2;

        if (PlayerPrefs.GetString("lv1Passed") == "yes")
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
