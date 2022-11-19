using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//开始界面
public class SelectUI : UIBase
{
    public Button PALADIN;
    public Button MONK;
    public Button SAMURAI;

    public Button GoToNext;


    private void Awake()
    {
        
        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("SelectUI播放BGM");
            AudioManager.Instance.PlayBGM("开场BGM");
        }
        LinkButtonToProfession();

        
    }

    //将3个按钮绑定对应事件
    private void LinkButtonToProfession()
    {
        PALADIN.onClick.AddListener(ChangeProfessionToPaladin);
        MONK.onClick.AddListener(ChangeProfessionToMonk);
        SAMURAI.onClick.AddListener(ChangeProfessionToSumarai);
    }
    //完成职业设置，显示职业描述
    public void ChangeProfessionToPaladin()
    {
        RoleManager.Instance.SetProfession(Professions.PALADIN);
        Debug.Log("将职业设置为圣骑");
        DescriptionManager.Instance.ChangePerfessionDescription(Professions.PALADIN);
        
        //在一开始将技能设置为normal级别
        RoleManager.Instance.SetProSkillLvl(SkillLevel.NORMAL);

        //选择职业后，才可以进入下一步
        GoToNext.onClick.AddListener(gotoNext);
        GoToNext.gameObject.SetActive(true);
    }
    public void ChangeProfessionToMonk()
    {
        RoleManager.Instance.SetProfession(Professions.MONK);
        Debug.Log("将职业设置为苦修");
        DescriptionManager.Instance.ChangePerfessionDescription(Professions.MONK);

        //在一开始将技能设置为normal级别
        RoleManager.Instance.SetProSkillLvl(SkillLevel.NORMAL);

        //选择职业后，才可以进入下一步
        GoToNext.onClick.AddListener(gotoNext);
        GoToNext.gameObject.SetActive(true);
    }

    public void ChangeProfessionToSumarai()
    {
        RoleManager.Instance.SetProfession(Professions.SAMURAI);
        Debug.Log("将职业设置为武士");
        DescriptionManager.Instance.ChangePerfessionDescription(Professions.SAMURAI);
        
        //在一开始将技能设置为normal级别
        RoleManager.Instance.SetProSkillLvl(SkillLevel.NORMAL);

        //选择职业后，才可以进入下一步
        GoToNext.onClick.AddListener(gotoNext);
        GoToNext.gameObject.SetActive(true);
    }

    public void gotoNext()
    {
        SceneManager.LoadScene("selectCardSkills");
    }
}