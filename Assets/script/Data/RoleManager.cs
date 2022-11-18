using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�û���Ϣ������(�û��ĵ�ǰ����)

public enum SkillLevel {NONE, NORMAL, RARE, EPIC, LEGENDARY, KNIGHT, MONK, SAMURAI};
public enum Professions {NONE, KNIGHT, MONK, SAMURAI }
public class RoleManager : MonoBehaviour
{
    public static RoleManager Instance = new RoleManager();

    //��ҵļ��ܽ�����Ϣ
    private Dictionary<KeyValuePair<string, SkillLevel>, bool> PlayerSkillStatus;
    //��ҵ�ǰѡ��ļ��ܵȼ��б�
    private Dictionary<string, SkillLevel> PlayerCurrentLevels;

    private Professions PlayerProfession;


    public void Awake()
    {
        PlayerProfession = Professions.NONE;
        SkillInit();
    }

    public void SetProfession(Professions pro)
    {
        PlayerProfession = pro;
    }

    public Professions GetProfession()
    {
        return PlayerProfession;
    }

    //�������õ�ǰ���ѡ��ļ��ܵȼ�, ����ֵ�����Ƿ����óɹ�
    public bool SetSkillLevel(string Key, SkillLevel level)
    {
        if (level == SkillLevel.NONE)
            return false;

        if (PlayerSkillStatus[new KeyValuePair<string, SkillLevel>(Key, level)] == true)
        {
            PlayerCurrentLevels[Key] = level;
            return true;
        }
        else
            return false;
    }

    //�õ���ҵ�ǰĳ�����ܵĵȼ�
    public SkillLevel GetCurrentSkillLevel(string Key)
    {
        return PlayerCurrentLevels[Key];
    }


    //���ݼ���id�͵ȼ��������ܵȼ�
    public void UnlockSkill(string Key, SkillLevel level)
    {
        PlayerSkillStatus[new KeyValuePair<string, SkillLevel>(Key, level)] = true;
    }

    //�õ���ҵļ��ܽ�����
    public Dictionary<KeyValuePair<string, SkillLevel>, bool> GetAllStatus()
    {
        return PlayerSkillStatus;
    }

    //�õ���ҵ�������ĳ�ȼ��Ľ���״��
    public bool GetSkillStatus(string Key, SkillLevel level)
    {
        return PlayerSkillStatus[new KeyValuePair<string, SkillLevel>(Key, level)];
    }



    


    //��ʼ������
    private void SkillInit()
    {
       PlayerSkillStatus = new Dictionary<KeyValuePair<string, SkillLevel>, bool> {
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.KNIGHT) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.SAMURAI) ,false},
       
       };



        PlayerCurrentLevels.Add("000", SkillLevel.NONE);
        PlayerCurrentLevels.Add("001", SkillLevel.NONE);
        PlayerCurrentLevels.Add("010", SkillLevel.NONE);
        PlayerCurrentLevels.Add("011", SkillLevel.NONE);
        PlayerCurrentLevels.Add("100", SkillLevel.NONE);
        PlayerCurrentLevels.Add("101", SkillLevel.NONE);
        PlayerCurrentLevels.Add("110", SkillLevel.NONE);
        PlayerCurrentLevels.Add("111", SkillLevel.NONE);

    }
}
