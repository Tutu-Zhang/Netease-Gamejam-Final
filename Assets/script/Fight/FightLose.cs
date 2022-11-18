using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightLose : FightUnit
{
    public Button BackToSelect;
    public Button ReBuildGame;
    public GameObject GoToEnd2;
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        Debug.Log("ÓÎÏ·Ê§°Ü");
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayEffect("Ê§°Ü");

        Invoke("ShowWindow", 1f);

                                    
    }
    private void ShowWindow()
    {
        GameObject obj = GameObject.Find("/Canvas/GameWindow/GameLose");
        obj.SetActive(true);

        BackToSelect = GameObject.FindGameObjectWithTag("LoseGTL").GetComponent<Button>();
        BackToSelect.onClick.AddListener(GoToSelectScence);
        ReBuildGame = GameObject.FindGameObjectWithTag("LoseGTS").GetComponent<Button>();
        ReBuildGame.onClick.AddListener(GoToReBuildGame);

        if (LevelManager.Instance.level == 4)
        {
            GoToEnd2 = GameObject.Find("/Canvas/GameWindow/GameLose/GotoEnd2");
            GoToEnd2.SetActive(true);
            Button btn = GoToEnd2.GetComponent<Button>();
            btn.onClick.AddListener(GoToEnd2Scence);
        }

        
    }

    private void GoToSelectScence()
    {
        AudioManager.Instance.PlayEffect("°´Å¥2");
        SceneManager.LoadScene("selectScene");
    }

    private void GoToReBuildGame()
    {
        AudioManager.Instance.PlayEffect("°´Å¥2");
        SceneManager.LoadScene("game1");
    }

    private void GoToEnd2Scence()
    {
        AudioManager.Instance.PlayEffect("°´Å¥2");
        SceneManager.LoadScene("AfterGame");
    }
    public override void OnUpdate()
    {

    }
}
