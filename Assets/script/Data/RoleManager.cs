using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�û���Ϣ������(�û��ĵ�ǰ����)

public enum SkillLevel {NONE, NORMAL, RARE, EPIC, LEGENDARY, PALADIN, MONK, SAMURAI};
public enum Professions {NONE, PALADIN, MONK, SAMURAI }
public class RoleManager : MonoBehaviour
{
    public static RoleManager Instance;
    //�����µļ������Ե��У�ÿ��level���Ӧһ��Status���Ӿ����㿴������һ��ľ���һ����Ӧ�飬���һ��levelû�ж�Ӧ��status����˵��������Բ���Ҫ���һ�������б�


    //��ҵĿ��Ƽ��ܽ�����Ϣ
    private Dictionary<KeyValuePair<string, SkillLevel>, bool> PlayerCardStatus;
    //��ҵ�ǰѡ��Ŀ��Ƽ��ܵȼ��б�
    public Dictionary<string, SkillLevel> PlayerCurrentLevels;


    //��ҵĵ�ǰְҵ��ְҵ���ܵȼ�
    public Professions PlayerProfession = Professions.NONE;
    public SkillLevel ProfessionSkillLevel = SkillLevel.NONE;//��ǰ��ְҵ���ܵȼ�


    //��ҵı�������������뵱ǰѡ��ı���
    private List<TreasureItem> Treasures;   
    public TreasureItem Treasure_1, Treasure_2;//1�ǹ�����2��ְҵ

    public void Start()
    {
        Instance = this;
        RoleManager.Instance.PlayerProfession = Professions.NONE;
        RoleManager.Instance.ProfessionSkillLevel = SkillLevel.NONE;
        RoleManager.Instance.CardTreasureInit();
    }

    //����num�����ҵ�ǰ��num���������ĸ�����
    public TreasureItem GetTreasure(int num)
    {
        if (num == 1)
            return Treasure_1;
        else if (num == 2)
            return Treasure_2;
        else
            return null;
    }

    //������ҵĵ�ǰЯ���ı���
    //�˺�������Ҫ�Ĳ���:��һ��ΪҪ�Ž���1���2������� �ڶ���ΪҪ���ĸ������ȥ��
    //ÿ�����������������ض�
    public void SetTreasure(int num, TreasureItem item)
    {
        if (num == 1)
            Treasure_1 = item;
        else if (num == 2)
            Treasure_2 = item;
        else
        {
            Debug.LogError("���õı�������ǵ�һ�����ߵڶ���");
        }
    }

    //���ĳ������Ľ������, ����δ���������Ϊ�����ڵı��ﶼ�᷵��false�������ڵı��ﶼ�Ǳ���ְҵ��Ϊͨ����ϡ�жȲ�Ϊ����ı���
    public bool GetTreasureLockStatus(TreasurePro p, TreasureLevel l, TreasureCategory c)
    {
        for(int i = 0;i < Treasures.Count; i++)
        {
            if(Treasures[i].TCategory == c && Treasures[i].TPro == p && Treasures[i].Tlevel == l)
            {
                return Treasures[i].IfUnlock;
            }
        }

        return false;
    }

    //����ĳ���ض�����
    public void UnlockTreasure(TreasurePro p, TreasureLevel l, TreasureCategory c)
    {
        for (int i = 0; i < Treasures.Count; i++)
        {
            if (Treasures[i].TCategory == c && Treasures[i].TPro == p && Treasures[i].Tlevel == l)
            {
                Treasures[i].IfUnlock = true;
            }
        }
    }

    //���ĳ���ض���������
    public TreasureItem GetTreasureStatus(TreasurePro p, TreasureLevel l, TreasureCategory c)
    {
        for (int i = 0; i < Treasures.Count; i++)
        {
            if (Treasures[i].TCategory == c && Treasures[i].TPro == p && Treasures[i].Tlevel == l)
            {
                return Treasures[i];
            }
        }

        return null;
    }

    //�������ְҵ���ܵȼ�
    public void SetProSkillLvl(SkillLevel lvl)
    {
        ProfessionSkillLevel = lvl;
    }
        
    public SkillLevel GetProSkillLvl()
    {
        return Instance.ProfessionSkillLevel;
    }

    //�������ְҵ
    public void SetProfession(Professions pro)
    {
        PlayerProfession = pro;
    }

    public Professions GetProfession()
    {
        return Instance.PlayerProfession;
    }

    //�������õ�ǰ���ѡ��Ŀ��Ƶȼ�, ����ֵ�����Ƿ����óɹ�
    public bool SetCardLevel_bool(string Key, SkillLevel level)
    {
        if (level == SkillLevel.NONE)
            return false;

        if (PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)] == true)
        {
            PlayerCurrentLevels[Key] = level;
            return true;
        }
        else
            return false;
    }

    //���ÿ��Ƽ��ܵȼ�
    public void SetCardLevel(string Key, SkillLevel level)
    {
        if (PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)] == true)
        {
            PlayerCurrentLevels[Key] = level;
            Debug.Log("���ܵȼ����óɹ�");
        }
        else
        {
            Debug.Log("����δ����");
        }

    }


    //�õ���ҵ�ǰĳ�����ܵĵȼ�
    public SkillLevel GetCurrentCardLevel(string Key)
    {
        if (PlayerCurrentLevels[Key] != null)
            return PlayerCurrentLevels[Key];
        else
            return SkillLevel.NONE;
    }


    //���ݼ���id�͵ȼ��������ܵȼ�
    public void UnlockCardLevel(string Key, SkillLevel level)
    {
        PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)] = true;
    }

    //�õ���ҵļ��ܽ�����
    public Dictionary<KeyValuePair<string, SkillLevel>, bool> GetAllStatus()
    {
        return PlayerCardStatus;
    }

    //�õ���ҵ�������ĳ�ȼ��Ľ���״��
    public bool GetCardStatus(string Key, SkillLevel level)
    {
        return PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)];
    }

    public bool GetProCardStatus(string Key, SkillLevel pro)
    {
        switch (pro)
        {
            case SkillLevel.PALADIN:
                if (Key == "000" || Key == "001" || Key == "010" || Key == "100")
                    return true;
                break;

            case SkillLevel.MONK:
                if (Key == "011" || Key == "110" || Key == "010" || Key == "100")
                    return true;
                break;

            case SkillLevel.SAMURAI:
                if (Key == "011" || Key == "110" || Key == "101" || Key == "111")
                    return true;
                break;
        }

        return false;
    }

    //��ʼ�����ƿ��뱦���
    public void CardTreasureInit()
    {
       PlayerCardStatus = new Dictionary<KeyValuePair<string, SkillLevel>, bool> 
       {
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("000", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("011", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("100", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.NONE) ,true},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.NORMAL) ,true},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("111", SkillLevel.SAMURAI) ,false},
       
       };

        PlayerCurrentLevels = new Dictionary<string, SkillLevel>();
        PlayerCurrentLevels.Add("000", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("001", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("010", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("011", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("100", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("101", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("110", SkillLevel.NORMAL);
        PlayerCurrentLevels.Add("111", SkillLevel.NORMAL);

        Treasures = new List<TreasureItem>();
        Treasures.Add(new TreasureItem(TreasureCategory.BUFF, TreasureLevel.RARE, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.BUFF, TreasureLevel.EPIC, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.PALADIN));
        Treasures.Add(new TreasureItem(TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.MONK));
        Treasures.Add(new TreasureItem(TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.SAMURAI));

        Treasures.Add(new TreasureItem(TreasureCategory.ROUND, TreasureLevel.RARE, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.ROUND, TreasureLevel.EPIC, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.PALADIN));
        Treasures.Add(new TreasureItem(TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.MONK));
        Treasures.Add(new TreasureItem(TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.SAMURAI)); ;

        Treasures.Add(new TreasureItem(TreasureCategory.PERGAME, TreasureLevel.RARE, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.PERGAME, TreasureLevel.EPIC, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.GENERAL));
        Treasures.Add(new TreasureItem(TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.PALADIN));
        Treasures.Add(new TreasureItem(TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.MONK));
        Treasures.Add(new TreasureItem(TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.SAMURAI)); ;

        Debug.Log("���Ƴ�ʼ�����");   
    }

}
