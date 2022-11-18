using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���＼�ܷ��棬��ʿ����Ϊ��һ�غ�buff��������Ϊ��ʱ���㼼��
public class PlayerSkill
{

    public static void MatchSkill(Professions pro, SkillLevel level)
    {
        switch (pro)
        {
            case Professions.PALADIN:
                PaladinSkill(level);
                break;

            case Professions.MONK:
                MonkSkill(level);
                break;

            case Professions.SAMURAI:
                SamuraiSkill(level);
                break;
        }
    }


    public static void PaladinSkill(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.GetDefendRecover(4);
                break;

            case SkillLevel.RARE:
                FightManager.Instance.GetDefendRecover(6);
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.GetDefendRecover(8);
                break;

            case SkillLevel.LEGENDARY:
                FightManager.Instance.GetDefendRecover(10);
                break;

        }
    }

    public static void MonkSkill(SkillLevel level)
    {
        switch (level)
        {
            case SkillLevel.NORMAL:
                FightManager.Instance.Attack_Enemy(4);
                FightManager.Instance.GetPlayHit(2);
                break;

            case SkillLevel.RARE:
                FightManager.Instance.Attack_Enemy(8);
                FightManager.Instance.GetPlayHit(4);
                break;

            case SkillLevel.EPIC:
                FightManager.Instance.Attack_Enemy(10);
                FightManager.Instance.GetPlayHit(6);
                break;

            case SkillLevel.LEGENDARY:
                FightManager.Instance.Attack_Enemy(12);
                FightManager.Instance.GetPlayHit(8);
                break;

        }
    }

    public static void SamuraiSkill(SkillLevel level)
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("SamuraiSkill", level, 1);
    }


}
