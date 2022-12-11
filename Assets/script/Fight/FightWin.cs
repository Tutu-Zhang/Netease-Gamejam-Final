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
        Debug.Log("��Ϸʤ��");
        AudioManager.Instance.StopBGM();        
        AudioManager.Instance.PlayEffect("ʤ��");

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
        OpenTreasure = GameObject.FindGameObjectWithTag("WinGTL").GetComponent<Button>();//�ҵ��챦��ť
        OpenTreasure.onClick.AddListener(GoToAfterGameScence);
    }


    private void GoToAfterGameScence()
    {
        AudioManager.Instance.PlayEffect("��ť2");
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
