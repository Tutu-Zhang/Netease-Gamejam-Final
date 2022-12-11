using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureEffects
{
    //根据三属性执行一个特定宝物效果
    public static void MatchTreasure(TreasurePro pro, TreasureCategory category, TreasureLevel level)
    {
        if(pro == TreasurePro.GENERAL)
        {
            switch (category)
            {
                case TreasureCategory.BUFF:
                    General_Buff(level);
                    break;

                case TreasureCategory.ROUND:
                    General_Round(level);
                    break;

                case TreasureCategory.PERGAME:
                    General_PerGame(level);
                    break;
            }
        }
        else //以上是通用宝物，以下是职业专属宝物，职业专属宝物的稀有度没有意义
        {
            switch (pro)
            {
                case TreasurePro.PALADIN:
                    Debug.Log("执行圣骑士宝物");
                    PaladinTreasure(category);
                    break;

                case TreasurePro.MONK:
                    MonkTreasure(category);
                    break;

                case TreasurePro.SAMURAI:
                    SamuraiTreasure(category);
                    break;
            }
        }
    }

    //以General开头的代表通用宝物，后面的为宝物类型，buff是buff型, round是三回合型, Pergame是整局游戏型
    public static void General_Buff(TreasureLevel level)
    {
        switch (level)
        {
            case TreasureLevel.RARE:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("BuffTreasure", SkillLevel.RARE, 999);
                break;

            case TreasureLevel.EPIC:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("BuffTreasure", SkillLevel.EPIC, 999);
                break;

            case TreasureLevel.LEGEND:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("BuffTreasure", SkillLevel.LEGENDARY, 999);
                break;
        }
    }

    public static void General_Round(TreasureLevel level)
    {
        switch (level)
        {
            case TreasureLevel.RARE:
                FightManager.Instance.GetRecover(8);
                break;

            case TreasureLevel.EPIC:
                int drawCardCount = 6 - UIManager.Instance.GetUI<FightUI>("fightBackground").GetCardNum() - UIManager.Instance.GetUI<FightUI>("fightBackground").GetPlayCardNum();
                Debug.Log("抽到了"+drawCardCount+"张牌");
                UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//补满卡牌
                break;

            case TreasureLevel.LEGEND:
                FightManager.Instance.MaxHP += 10;
                FightManager.Instance.GetRecover(10);
                break;
        }
    }

    public static void General_PerGame(TreasureLevel level)
    {
        switch (level)
        {
            case TreasureLevel.RARE:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("PergameTreasure", SkillLevel.RARE, 1);
                break;

            case TreasureLevel.EPIC:
                FightManager.Instance.Attack_Enemy(20);
                break;

            case TreasureLevel.LEGEND:
                if(FightManager.Instance.CurHP < FightManager.Instance.MaxHP)
                {
                    FightManager.Instance.CurHP = FightManager.Instance.MaxHP;
                }
                break;
        }
    }

    //圣骑士专属宝物
    public static void PaladinTreasure(TreasureCategory ctg)
    {
        switch (ctg)
        {
            case TreasureCategory.BUFF:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("PaladinBuff", SkillLevel.LEGENDARY, 999);
                break;

            case TreasureCategory.ROUND:
                FightManager.Instance.Attack_Enemy(FightManager.Instance.DefCount);
                break;

            case TreasureCategory.PERGAME:
                FightManager.Instance.GetDefendRecover(FightManager.Instance.CurHP - 1);
                FightManager.Instance.CurHP = 1;
                UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();

                break;
        }
    }

    //苦修宝物，其中一整局一次的宝物本质上就是999回合的苦修110牌
    public static void MonkTreasure(TreasureCategory ctg)
    {
        switch (ctg)
        {
            case TreasureCategory.BUFF:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("MonkBuff", SkillLevel.LEGENDARY, 999);
                break;

            case TreasureCategory.ROUND:
                FightManager.Instance.Attack_Enemy(FightManager.Instance.MaxHP - FightManager.Instance.CurHP);
                break;

            case TreasureCategory.PERGAME:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("110", SkillLevel.MONK, 999);
                break;
        }
    }

    //武士宝物
    public static void SamuraiTreasure(TreasureCategory ctg)
    {
        switch (ctg)
        {
            case TreasureCategory.BUFF:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("SamuraiBuff", SkillLevel.LEGENDARY, 999);
                break;

            case TreasureCategory.ROUND:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("SamuraiRound", SkillLevel.LEGENDARY, 1);
                break;

            case TreasureCategory.PERGAME:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("SamuraiPergame", SkillLevel.LEGENDARY, 999);
                break;
        }
    }


}
