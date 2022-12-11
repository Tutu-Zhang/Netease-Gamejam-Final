using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureEffects
{
    //����������ִ��һ���ض�����Ч��
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
        else //������ͨ�ñ��������ְҵר�����ְҵר�������ϡ�ж�û������
        {
            switch (pro)
            {
                case TreasurePro.PALADIN:
                    Debug.Log("ִ��ʥ��ʿ����");
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

    //��General��ͷ�Ĵ���ͨ�ñ�������Ϊ�������ͣ�buff��buff��, round�����غ���, Pergame��������Ϸ��
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
                Debug.Log("�鵽��"+drawCardCount+"����");
                UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//��������
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

    //ʥ��ʿר������
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

    //���ޱ������һ����һ�εı��ﱾ���Ͼ���999�غϵĿ���110��
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

    //��ʿ����
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
