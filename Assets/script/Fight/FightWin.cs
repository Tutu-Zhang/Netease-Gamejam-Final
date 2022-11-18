using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightWin : FightUnit
{
    public Button BackToSelect;
    public Button GoToAfterGame;

    public override void Init()
    {
        //如果第四关胜利，则触发战胜剧情，否则触发战败剧情
        if (LevelManager.Instance.level == 4)
        {
            LevelManager.Instance.level = 5;
        }

        FightManager.Instance.StopAllCoroutines();
        Debug.Log("游戏胜利");
        AudioManager.Instance.StopBGM();        
        AudioManager.Instance.PlayEffect("胜利");

        //保存通关数据
        if (LevelManager.Instance.level == 0)
        {
            PlayerPrefs.SetString("lv0Passed", "yes");
        }
        else
        {
            PlayerPrefs.SetString("lv" + LevelManager.Instance.level.ToString() + "Passed", "yes");
        }
        
        PlayerPrefs.Save();

        Invoke("ShowWindow", 1f);
    }

    private void ShowWindow()
    {
        GameObject obj = GameObject.Find("/Canvas/GameWindow/GameWin");
        obj.SetActive(true);

        BackToSelect = GameObject.FindGameObjectWithTag("WinGTL").GetComponent<Button>();
        BackToSelect.onClick.AddListener(GoToSelectScence);
        GoToAfterGame = GameObject.FindGameObjectWithTag("WinGTS").GetComponent<Button>();
        GoToAfterGame.onClick.AddListener(GoToAfterGameScence);
    }

    private void GoToSelectScence()
    {
        AudioManager.Instance.PlayEffect("按钮2");
        SceneManager.LoadScene("selectScene");
    }

    private void GoToAfterGameScence()
    {
        AudioManager.Instance.PlayEffect("按钮2");
        SceneManager.LoadScene("AfterGame");
    }

    public override void OnUpdate()
    {

    }
}
