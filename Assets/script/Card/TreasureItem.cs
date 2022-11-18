using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

//宝物稀有度
public enum TreasureLevel {RARE, EPIC, LEGEND }

//宝物的种类
public enum TreasureCategory {BUFF, ROUND, PERGAME}

//宝物的职业所属，通用的宝物职业所属为GENERAL
public enum TreasurePro {GENERAL, PALADIN, MONK, SAMURAI }

public class TreasureItem
{
    public TreasureLevel Tlevel;
    public TreasureCategory TCategory;
    public TreasurePro TPro;

    //以上三个属性对应宝物稀有等级，宝物种类，宝物的职业，通过这三个属性可以唯一确定一个宝物，不过当职业属性
    //不为general时，宝物的稀有度没有意义，此时通过职业和种类就可确定一个宝物

    public bool Ifready = true;//这个代表宝物是否可用
    public bool IfUnlock = false;//这个代表是否解锁
    private int PrepareTime = 0;//这个代表还有几回合可用


    public TreasureItem(TreasureCategory category, TreasureLevel level, TreasurePro pro)
    {
        Tlevel = level;
        TCategory = category;
        TPro = pro;
    }

    public void TPassTurn()
    {
        if(PrepareTime > 0)
            PrepareTime -= 1;
        if (PrepareTime == 0)
            Ifready = true;
    }

    //每次使用宝物都要调用一次此函数
    public void UseTreasure()
    {
        if (!Ifready)
        {
            Debug.Log("宝物不可用");
            return;
        }
            

        if(TCategory == TreasureCategory.ROUND)
        {
            Ifready = false;
            PrepareTime = 3;
        }
        else 
        {
            Ifready = false;
            PrepareTime = 999;
        }

        TreasureEffects.MatchTreasure(TPro, TCategory, Tlevel);
    }

    //每局游戏开始前，需要对所有宝物执行一次此函数以保证宝物下回合可用
    public void SetTreasureAble()
    {
        Ifready = true;
        PrepareTime = 0;
    }

    public void SetPreTime(int i)
    {
        PrepareTime = i;
    }

    public int GetPreTime()
    {
        return PrepareTime;
    }



}
