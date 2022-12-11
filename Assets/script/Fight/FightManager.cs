using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗类型枚举
public enum FightType
{
    None,
    Init,
    Player,//玩家回合
    Enemy,//敌人回合
    Win,
    Lose
}
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;

    GameObject turnBtn;

    public int MaxHP;
    public int CurHP;


    public int DefCount;//护甲
    public int TurnCount;//回合数,这个回合数是每到玩家回合加一次的



    private void Start()
    {
        turnBtn = GameObject.Find("turnBtn");
    }

    public void Init()//玩家起始数据
    {
        MaxHP = 20 + LevelManager.Instance.MaxHpFix * 10;//根据提升血量上限的次数（MaxHpFix）调整血量
        CurHP = 20 + LevelManager.Instance.MaxHpFix * 10; ;
        DefCount = 10;
        TurnCount = 0;

    }

    private void Awake()
    {
        Instance = this;
    }

    //切换战斗类型
    public void ChangeType(FightType type)
    {
        GameObject obj = new GameObject(type.ToString());
        obj.transform.parent = GameObject.Find("TurnSets").transform;
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = obj.AddComponent<FightInit>();
                break;
            case FightType.Player:
                fightUnit = obj.AddComponent<FightPlayerTurn>();
                break;
            case FightType.Enemy:
                fightUnit = obj.AddComponent<FightEnemyTurn>();
                break;
            case FightType.Win:
                fightUnit = obj.AddComponent <FightWin>();
                break;
            case FightType.Lose:
                GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
                enemy.SetActive(false);
                fightUnit = obj.AddComponent <FightLose>();
                break;
        }
        fightUnit.Init();
        if(type == FightType.Player)
        {

            TurnCount++;
        }
    }

    public int GetTurnCount()
    {
        return TurnCount;
    }

    //玩家受伤逻辑
    public void GetPlayHit(int hit)
    {
        int this_hit = hit + LevelManager.Instance.AttackFix;

        //苦修宝物Buff
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("MonkBuff", SkillLevel.LEGENDARY) != null)
        {
            this_hit += BuffEffects.buff_MonkBuff();
        }
        
        //011减伤免伤buff
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("011") != null)
        {
            this_hit = BuffEffects.buff_Defend_011(this_hit, UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("011").GetBuffLevel());
        }

        //武士宝物buff
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("SamuraiBuff", SkillLevel.LEGENDARY) != null)
        {
            this_hit = BuffEffects.buff_SamuraiBuff(this_hit);
        }

        //苦修110牌
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("110", SkillLevel.MONK) != null)
        {
            BuffEffects.buff_counter_110(this_hit, SkillLevel.MONK);
        }


        //优先扣护盾
        if (DefCount >= this_hit)
        {
            DefCount -= this_hit;
        }
        else
        {
            this_hit = this_hit - DefCount;
            DefCount = 0;
            CurHP -= this_hit;
            if (CurHP <= 0)
            {
                CurHP = 0;
                //删除敌人模型
                GameObject enemyModel = GameObject.FindGameObjectWithTag("Enemy");  
                Debug.Log("找到敌人模型");
                Destroy(enemyModel);
                
                //切换到游戏失败状态
                ChangeType(FightType.Lose);
            }
        }

        //更新界面
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetRecover(int recover)
    {
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("001", SkillLevel.LEGENDARY) != null)
        { recover *= 2; }

        AudioManager.Instance.PlayEffect("回复");
        CurHP += recover;

        if (CurHP > MaxHP) {
            if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("BuffTreasure", SkillLevel.EPIC) != null)
            {
                Attack_Enemy(BuffEffects.buff_BuffTreasure(SkillLevel.EPIC));
            }
                CurHP = MaxHP; 
        }

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetDefendRecover(int recover)
    {
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("001", SkillLevel.LEGENDARY) != null)
        { recover *= 2; }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("011", SkillLevel.SAMURAI) != null)
        {
            Attack_Enemy(recover * 2);
            AudioManager.Instance.PlayEffect("玩家攻击");
            return;
        }
        //圣骑士宝物Buff
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("PaladinBuff", SkillLevel.LEGENDARY) != null)
        {
            for (int i = 0; i < recover; i++) { 
                BuffEffects.PaladinBuffCount += 1;
                BuffEffects.buff_PaladinBuff();
            }
        }

        AudioManager.Instance.PlayEffect("护甲");
        DefCount += recover;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetDefendDecrease(int val)
    {
        if (DefCount >= val)
        {
            DefCount -= val;
        }
        else
        {
            DefCount = 0;
        }
    }

    //扣除敌人护甲
    public void ArmorBreak(int val)
    {
        AudioManager.Instance.PlayEffect("破甲");

        EnemyManager.Instance.GetEnemy(0).DefendDecrease(val);
    }

    public void Attack_Enemy(int val)
    {
        AudioManager.Instance.PlayEffect("玩家攻击");
        Debug.Log("AttackEnemy执行");

        int this_dmg = val;

        //苦修宝物buff
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("MonkBuff", SkillLevel.LEGENDARY) != null)
        {
            this_dmg += BuffEffects.buff_MonkBuff();
        }
        //稀有buff类宝物
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("BuffTreasure", SkillLevel.RARE) != null)
        {
            this_dmg += BuffEffects.buff_BuffTreasure(SkillLevel.RARE);
        }
        //武士技能
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("SamuraiSkill") != null)
        {
            this_dmg += BuffEffects.buff_Samurai_skill(UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("SamuraiSkill").GetBuffLevel());
            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("SamuraiSkill"));
        }
        //武士110
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("110", SkillLevel.SAMURAI) != null)
        {
            this_dmg = BuffEffects.buff_counter_110(this_dmg, SkillLevel.SAMURAI);
            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("110", SkillLevel.SAMURAI));
        }
        //武士3回合宝物
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("SamuraiRound", SkillLevel.LEGENDARY) != null)
        {
            this_dmg = BuffEffects.buff_SamuraiRound(this_dmg);
            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("SamuraiRound", SkillLevel.LEGENDARY));
        }
        //传奇111
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("111", SkillLevel.LEGENDARY) != null)
        {
            this_dmg = BuffEffects.buff_legend_111(this_dmg);
        }
        //武士PergameBuff, 此buff没有写在BuffEffects文件中
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("SamuraiPergame", SkillLevel.LEGENDARY) != null)
        {
            EnemyManager.Instance.GetEnemy(0).ThroughHited(this_dmg);
            return;
        }


        EnemyManager.Instance.GetEnemy(0).Hited(this_dmg);
        
    }

    public void SetBtn(bool option)
    {
        turnBtn.SetActive(option);
    }

    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }
}
