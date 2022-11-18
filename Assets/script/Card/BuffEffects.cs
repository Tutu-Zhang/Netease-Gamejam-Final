using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffects
{

    public static int Buff_101_Legend = 0;

    //每回合开始时会检测上回合是否使用过111，使用过则此属性将为真，没有则是假。如果为真则将其至假，为假则将增伤至0
    public static bool Buff_111_SAMURAL_IFUSED = false;
    public static int Buff_111_SAMURAI_DMGINCREASE = 0;

    //纪录回复了多少护甲的变量,用于PALADIN的buff宝物
    public static int PaladinBuffCount = 0;

    public static void MatchBuff(string buffid, SkillLevel buffLevel)
    {
        switch (buffid)
        {
            case "001":
                buff_recover_001(buffLevel);
                break;

            case "010":
                buff_Recover_Defend_010();
                break;

            case "101":
                buff_attack_101(buffLevel);
                break;

            case "111":
                buff_attack_111(buffLevel);
                break;


        }
    }

    //Buff001
    public static void buff_recover_001(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.GetDefendRecover(2);
                break;

            case SkillLevel.RARE:
                FightManager.Instance.GetDefendRecover(4);
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.GetRecover(4);
                break;

            case SkillLevel.LEGENDARY:
                break;

            case SkillLevel.PALADIN:
                FightManager.Instance.GetDefendRecover(3);
                FightManager.Instance.GetRecover(3);
                break;
        }
    }

    //Buff010,仅有传奇等级有BUFF
    public static void buff_Recover_Defend_010()
    {
        FightManager.Instance.GetDefendDecrease(5);
    }



    //011Buff效果
    public static int buff_Defend_011(int damage, SkillLevel level)
    {
        int this_dmg = damage;

        switch (level)
        {
            case SkillLevel.NORMAL:
                this_dmg -= 3;
                this_dmg = CardEffects.TimesByRate(this_dmg, 0.75f, 0);
                return this_dmg;

            case SkillLevel.RARE:
                this_dmg -= 6;
                this_dmg = CardEffects.TimesByRate(this_dmg, 0.75f, 0);
                return this_dmg;

            case SkillLevel.EPIC:
                this_dmg -= 3;
                this_dmg = CardEffects.TimesByRate(this_dmg, 0.5f, 0);
                return this_dmg;

            case SkillLevel.LEGENDARY:
                this_dmg = 0;
                return this_dmg;

            case SkillLevel.MONK:
                if (FightManager.Instance.CurHP + FightManager.Instance.DefCount > this_dmg)
                    return this_dmg;
                else
                    return FightManager.Instance.CurHP + FightManager.Instance.DefCount - 1;

            default:
                return this_dmg;

        }

    }

    //110MONK SAMURAL buff
    public static int buff_counter_110(int dmg, SkillLevel level)
    {
        if (level == SkillLevel.MONK) {
            FightManager.Instance.Attack_Enemy(dmg); 
        }
        else if (level == SkillLevel.SAMURAI)
        {
            return dmg * 3;
        }

        return 1;
    }



    public static void buff_attack_101(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.Attack_Enemy(3);
                break;

            case SkillLevel.RARE:
                FightManager.Instance.Attack_Enemy(6);
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.Attack_Enemy(3);
                FightManager.Instance.GetRecover(3);
                break;

            case SkillLevel.LEGENDARY:
                if(Buff_101_Legend > 30 
                    //&& UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("101").GetBuffLevel() == SkillLevel.LEGENDARY
                    && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("101").GetLeftTime() == 1)
                    FightManager.Instance.Attack_Enemy(15);
                break;

            case SkillLevel.SAMURAI:
                FightCardManager.Instance.SetPro_High();
                break;

        }
    }

    public static void buff_attack_111(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.Attack_Enemy(CardEffects.TimesByRate(4,0.5f,2));
                break;

            case SkillLevel.RARE:
                FightManager.Instance.Attack_Enemy(CardEffects.TimesByRate(8, 0.5f, 2));
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.ArmorBreak(CardEffects.TimesByRate(12, 0.5f, 2));
                break;

            case SkillLevel.LEGENDARY:
                break;
        }
    }

    public static int buff_legend_111(int dmg)
    {
        if(CardEffects.TimesByRate(1, 0.5f, 2) == 1)
        {
            return dmg * 2;
        }
        else
        {
            FightManager.Instance.GetRecover(dmg);
            return dmg;
        }
    }

    //从这里开始是人物专属技能部分
    public static int buff_Samurai_skill(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                return 4;

            case SkillLevel.RARE:
                return 6;

            case SkillLevel.EPIC:
                return 8;

            case SkillLevel.LEGENDARY:
                return 10;

            default:
                return 0;
        }
    }

    //从这里开始是宝物buff部分
    public static int buff_BuffTreasure(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.RARE:
                return 2;

            case SkillLevel.EPIC:
                return FightManager.Instance.CurHP - FightManager.Instance.MaxHP;

            case SkillLevel.LEGENDARY:
                return FightManager.Instance.TurnCount;

            default:
                return 0;
        }
    }

    public static void buff_PaladinBuff()
    {
        if(PaladinBuffCount % 6 == 0)
        {
            FightManager.Instance.Attack_Enemy(4);
        }
    }

    public static int buff_MonkBuff()
    {
        return 2;
    }

    public static int buff_SamuraiBuff(int dmg)
    {
        System.Random random = new System.Random();
        random.NextDouble();
        random.NextDouble();
        double temp = random.NextDouble();
        if (temp >= 0.9)
        {
            return 0;
        }
        return dmg;
    }

    public static int buff_SamuraiRound(int dmg)
    {
        return dmg * 3;
    }


    public static bool buff_PerTreasure_Rare()
    {
        return true;
    }

}
