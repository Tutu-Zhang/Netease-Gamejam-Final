using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//��ʼ����
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
            Debug.Log("SelectUI����BGM");
            AudioManager.Instance.PlayBGM("����BGM");
        }
        LinkButtonToProfession();

        
    }

    //��3����ť�󶨶�Ӧ�¼�
    private void LinkButtonToProfession()
    {
        PALADIN.onClick.AddListener(ChangeProfessionToPaladin);
        MONK.onClick.AddListener(ChangeProfessionToMonk);
        SAMURAI.onClick.AddListener(ChangeProfessionToSumarai);
    }
    //���ְҵ���ã���ʾְҵ����
    public void ChangeProfessionToPaladin()
    {
        RoleManager.Instance.SetProfession(Professions.PALADIN);
        Debug.Log("��ְҵ����Ϊʥ��");
        DescriptionManager.Instance.ChangePerfessionDescription(Professions.PALADIN);
        
        //��һ��ʼ����������Ϊnormal����
        RoleManager.Instance.SetProSkillLvl(SkillLevel.NORMAL);

        //ѡ��ְҵ�󣬲ſ��Խ�����һ��
        GoToNext.onClick.AddListener(gotoNext);
        GoToNext.gameObject.SetActive(true);
    }
    public void ChangeProfessionToMonk()
    {
        RoleManager.Instance.SetProfession(Professions.MONK);
        Debug.Log("��ְҵ����Ϊ����");
        DescriptionManager.Instance.ChangePerfessionDescription(Professions.MONK);

        //��һ��ʼ����������Ϊnormal����
        RoleManager.Instance.SetProSkillLvl(SkillLevel.NORMAL);

        //ѡ��ְҵ�󣬲ſ��Խ�����һ��
        GoToNext.onClick.AddListener(gotoNext);
        GoToNext.gameObject.SetActive(true);
    }

    public void ChangeProfessionToSumarai()
    {
        RoleManager.Instance.SetProfession(Professions.SAMURAI);
        Debug.Log("��ְҵ����Ϊ��ʿ");
        DescriptionManager.Instance.ChangePerfessionDescription(Professions.SAMURAI);
        
        //��һ��ʼ����������Ϊnormal����
        RoleManager.Instance.SetProSkillLvl(SkillLevel.NORMAL);

        //ѡ��ְҵ�󣬲ſ��Խ�����һ��
        GoToNext.onClick.AddListener(gotoNext);
        GoToNext.gameObject.SetActive(true);
    }

    public void gotoNext()
    {
        SceneManager.LoadScene("selectCardSkills");
    }
}