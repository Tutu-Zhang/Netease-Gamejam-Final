using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects
{
    public static void MatchCard(string cardid, SkillLevel level)
    {
        switch (cardid)
        {
            case "000":
                Recover_Defend_000(level);
                break;

            case "001":
                Defend_001(level);
                break;

            case "010":
                Recover_Defend_010(level);
                break;

            case "011":
                Defend_011(level);
                break;

            case "100":
                Attack_100(level);
                break;

            case "101":
                attack_101(level);
                break;

            case "110":
                Attack_110(level);
                break;

            case "111":
                attack_111(level);
                break;

        }
    }

    //卡牌000效果及其所需函数
    public static void Recover_Defend_000(SkillLevel level)
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
                gamble();
                break;

            case SkillLevel.PALADIN:
                FightManager.Instance.GetDefendRecover(6);
                break;
        }
    }
    public static void gamble()
    {
        System.Random random = new System.Random();

        //先洗一下伪随机
        for(int i = 0; i<5;i++)
            random.NextDouble();

        string card = "";

        while (card.Length < 3)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp > 0.5)
            {
                num = "1";
            }

            card += num;
        }

            MatchCard(card, SkillLevel.LEGENDARY);
    }



    //卡牌001效果及其所需函数
    public static void Defend_001(SkillLevel level)
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("001", level, 3);
    }



    //卡牌010效果及其所需函数
    public static void Recover_Defend_010(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.GetDefendRecover(TimesByRate(3, 0.5f, 2));
                break;

            case SkillLevel.RARE:
                FightManager.Instance.GetDefendRecover(TimesByRate(6, 0.5f, 2));
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.GetRecover(TimesByRate(3, 0.5f, 2));
                break;

            case SkillLevel.LEGENDARY:
                FightManager.Instance.GetDefendRecover(20);
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("010", SkillLevel.LEGENDARY, 2);
                break;

            case SkillLevel.PALADIN:
                FightManager.Instance.GetDefendRecover(2);
                FightManager.Instance.DefCount *= TimesByRate(1, 0.75f, 2);
                break;

            case SkillLevel.MONK:
                FightManager.Instance.GetRecover((FightManager.Instance.MaxHP - FightManager.Instance.CurHP)/TimesByRate(1, 0.2f, 2));
                break;
        }
    }

    public static int TimesByRate(int num, float rate, int times) //翻倍判定函数
    {
        int outNum = num;

        System.Random random = new System.Random();
        random.NextDouble();
        random.NextDouble();
        double temp = random.NextDouble();
        if (temp >= rate)
        {
            outNum *= times;
        }

        return outNum;
    }



    //卡牌100效果及其所需函数
    public static void Attack_100(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.Attack_Enemy(400);//调试用
                break;

            case SkillLevel.RARE:
                FightManager.Instance.Attack_Enemy(8);
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.ArmorBreak(12);
                break;

            case SkillLevel.LEGENDARY:
                FightManager.Instance.Attack_Enemy(8);
                FightManager.Instance.GetRecover(8);
                break;

            case SkillLevel.PALADIN:
                FightManager.Instance.Attack_Enemy(FightManager.Instance.DefCount);
                break;

            case SkillLevel.MONK:
                FightManager.Instance.Attack_Enemy(FightManager.Instance.MaxHP - FightManager.Instance.CurHP);
                break;
        }
    }




    //011
    public static void Defend_011(SkillLevel level)
    {
        if(level == SkillLevel.LEGENDARY || level == SkillLevel.MONK)
        {
            UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("011", level, 1);
        }
        else
            UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("011", level, 3);
    }



    //110卡牌效果
    public static void Attack_110(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.Attack_Enemy(TimesByRate(4, 0.5f, 2));
                break;

            case SkillLevel.RARE:
                FightManager.Instance.Attack_Enemy(TimesByRate(8, 0.5f, 2));
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.Attack_Enemy(TimesByRate(6, 0.25f, 2));
                break;

            case SkillLevel.LEGENDARY:
                if(EnemyManager.Instance.GetEnemy(0).Defend <= 0)
                {
                    FightManager.Instance.Attack_Enemy(32);
                }
                else
                    FightManager.Instance.Attack_Enemy(16);
                break;

            case SkillLevel.MONK:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("110",SkillLevel.MONK ,1);
                break;

            case SkillLevel.SAMURAI:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("110", SkillLevel.SAMURAI, 999);
                break;
        }
    }

    public static void attack_101(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.Attack_Enemy(3);
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("101", level, 2);
                break;

            case SkillLevel.RARE:
                FightManager.Instance.Attack_Enemy(6);
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("101", level, 2);
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.Attack_Enemy(3);
                FightManager.Instance.GetRecover(3);
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("101", level, 2);
                break;

            case SkillLevel.LEGENDARY:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("101", level, 4);
                break;

            case SkillLevel.SAMURAI:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("101", SkillLevel.SAMURAI, 2);
                break;
        }
    }


    public static void attack_111(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("111", level, 2);
                break;

            case SkillLevel.RARE:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("111", level, 2);
                break;

            case SkillLevel.EPIC:
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("111", level, 2);
                break;

            case SkillLevel.LEGENDARY:
                FightManager.Instance.Attack_Enemy(10);
                UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("111", level, 3);
                break;

            case SkillLevel.SAMURAI:
                if (!BuffEffects.Buff_111_SAMURAL_IFUSED)
                    BuffEffects.Buff_111_SAMURAI_DMGINCREASE = 0;
                else if (BuffEffects.Buff_111_SAMURAL_IFUSED)
                    BuffEffects.Buff_111_SAMURAL_IFUSED = false;


                FightManager.Instance.Attack_Enemy(10 + BuffEffects.Buff_111_SAMURAI_DMGINCREASE);
                if(BuffEffects.Buff_111_SAMURAI_DMGINCREASE < 20)
                    BuffEffects.Buff_111_SAMURAI_DMGINCREASE += 10;

                break;
        }
    }
}
