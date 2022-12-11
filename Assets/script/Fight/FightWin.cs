using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightWin : FightUnit
{
    public Button OpenTreasure;

    public override void Init()
    {

        FightManager.Instance.StopAllCoroutines();
        Debug.Log("游戏胜利");
        AudioManager.Instance.StopBGM();        
        AudioManager.Instance.PlayEffect("胜利");

        if (LevelManager.Instance.level == 12)
        {
            Invoke("GoToFinal", 3f);
        }
        else
        {
            LevelManager.Instance.level += 1;
            Invoke("ShowWindow", 1f);
        }

    }

    private void ShowWindow()
    {
        GameObject obj = GameObject.Find("/Canvas/GameWindow/GameWin");
        obj.SetActive(true);
        OpenTreasure = GameObject.FindGameObjectWithTag("WinGTL").GetComponent<Button>();//找到领宝按钮
        OpenTreasure.onClick.AddListener(GoToAfterGameScence);
    }


    private void GoToAfterGameScence()
    {
        AudioManager.Instance.PlayEffect("按钮2");
        SceneManager.LoadScene("winSelect");
    }

    private void GoToFinal()
    {
        SceneManager.LoadScene("FinalUI");
    }
    public override void OnUpdate()
    {

    }
}
