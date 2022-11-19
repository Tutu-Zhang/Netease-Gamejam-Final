using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//用户信息管理类(用户的当前属性)

public enum SkillLevel {NONE, NORMAL, RARE, EPIC, LEGENDARY, PALADIN, MONK, SAMURAI};
public enum Professions {NONE, PALADIN, MONK, SAMURAI }
public class RoleManager : MonoBehaviour
{
    public static RoleManager Instance;
    //在以下的几个属性当中，每个level会对应一个Status，视觉上你看起来在一起的就是一个对应组，如果一个level没有对应的status，就说明这个属性不需要设计一个解锁列表


    //玩家的卡牌技能解锁信息
    private Dictionary<KeyValuePair<string, SkillLevel>, bool> PlayerCardStatus;
    //玩家当前选择的卡牌技能等级列表
    private Dictionary<string, SkillLevel> PlayerCurrentLevels;


    //玩家的当前职业和职业技能等级
    public Professions PlayerProfession;
    public SkillLevel ProfessionSkillLevel;//当前的职业技能等级


    //玩家的宝物库与解锁情况与当前选择的宝物
    private List<TreasureItem> Treasures;
    private TreasureItem Treasure_1, Treasure_2;//1是公共，2是职业

    public void Start()
    {
        Instance = this;
        PlayerProfession = Professions.NONE;
        ProfessionSkillLevel = SkillLevel.NONE;
        CardTreasureInit();
    }

    //根据num获得玩家当前第num个宝物是哪个宝物
    public TreasureItem GetTreasure(int num)
    {
        if (num == 1)
            return Treasure_1;
        else if (num == 2)
            return Treasure_2;
        else
            return null;
    }

    //设置玩家的当前携带的宝物
    //此函数所需要的参数:第一个为要放进第1或第2个宝物框， 第二个为要放哪个宝物进去；
    //每个宝物由三个属性特定
    public void SetTreasure(int num, TreasureItem item)
    {
        if (num == 1)
            Treasure_1 = item;
        else if (num == 2)
            Treasure_2 = item;
        else
        {
            Debug.LogError("设置的宝物必须是第一个或者第二个");
        }
    }

    //获得某个宝物的解锁情况, 宝物未解锁或参数为不存在的宝物都会返回false，不存在的宝物都是宝物职业不为通用且稀有度不为传奇的宝物
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

    //解锁某个特定宝物
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

    //获得某个特定宝物的情况
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

    //设置玩家职业技能等级
    public void SetProSkillLvl(SkillLevel lvl)
    {
        ProfessionSkillLevel = lvl;
    }
        
    public SkillLevel GetProSkillLvl()
    {
        return Instance.ProfessionSkillLevel;
    }

    //设置玩家职业
    public void SetProfession(Professions pro)
    {
        PlayerProfession = pro;
    }

    public Professions GetProfession()
    {
        return Instance.PlayerProfession;
    }

    //用来设置当前玩家选择的卡牌等级, 返回值代表是否设置成功
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

    //设置卡牌技能等级
    public void SetCardLevel(string Key, SkillLevel level)
    {
        if (PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)] == true)
        {
            PlayerCurrentLevels[Key] = level;
        }
        Debug.Log("技能等级设置成功");
    }


    //得到玩家当前某个技能的等级
    public SkillLevel GetCurrentCardLevel(string Key)
    {
        if (PlayerCurrentLevels[Key] != null)
            return PlayerCurrentLevels[Key];
        else
            return SkillLevel.NONE;
    }


    //根据技能id和等级解锁技能等级
    public void UnlockCardLevel(string Key, SkillLevel level)
    {
        PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)] = true;
    }

    //得到玩家的技能解锁表
    public Dictionary<KeyValuePair<string, SkillLevel>, bool> GetAllStatus()
    {
        return PlayerCardStatus;
    }

    //得到玩家单个技能某等级的解锁状况
    public bool GetCardStatus(string Key, SkillLevel level)
    {
        return PlayerCardStatus[new KeyValuePair<string, SkillLevel>(Key, level)];
    }
    //初始化卡牌库与宝物库
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

           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.NONE) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("001", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.NONE) ,false},
           {new KeyValuePair<string, SkillLevel>("010", SkillLevel.NORMAL) ,false},
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

           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.NONE) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.NORMAL) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.RARE) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.EPIC) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.LEGENDARY) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.PALADIN) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.MONK) ,false},
           {new KeyValuePair<string, SkillLevel>("101", SkillLevel.SAMURAI) ,false},

           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.NONE) ,false},
           {new KeyValuePair<string, SkillLevel>("110", SkillLevel.NORMAL) ,false},
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
        PlayerCurrentLevels.Add("000", SkillLevel.NONE);
        PlayerCurrentLevels.Add("001", SkillLevel.NONE);
        PlayerCurrentLevels.Add("010", SkillLevel.NONE);
        PlayerCurrentLevels.Add("011", SkillLevel.NONE);
        PlayerCurrentLevels.Add("100", SkillLevel.NONE);
        PlayerCurrentLevels.Add("101", SkillLevel.NONE);
        PlayerCurrentLevels.Add("110", SkillLevel.NONE);
        PlayerCurrentLevels.Add("111", SkillLevel.NONE);

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

        Debug.Log("卡牌初始化完成");   
    }

}
