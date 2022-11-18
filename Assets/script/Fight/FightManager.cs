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
        MaxHP = 15;
        CurHP = 15;
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

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("011") != null)
        {
            this_hit = BuffEffects.buff_Defend_011(this_hit, UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("011").GetBuffLevel());
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("110") != null 
            && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("110").GetBuffLevel() == SkillLevel.MONK)
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
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001") != null 
            && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001").GetBuffLevel() == SkillLevel.LEGENDARY)
        { recover *= 2; }

        AudioManager.Instance.PlayEffect("回复");
        CurHP += recover;
        if (CurHP > MaxHP)
            CurHP = MaxHP;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetDefendRecover(int recover)
    {
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001") != null
        && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001").GetBuffLevel() == SkillLevel.LEGENDARY)
        { recover *= 2; }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("011") != null
        && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("011").GetBuffLevel() == SkillLevel.SAMURAI)
        {
            Attack_Enemy(recover * 2);
            AudioManager.Instance.PlayEffect("玩家攻击");
            return;
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

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("110") != null 
            && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("110").GetBuffLevel() == SkillLevel.SAMURAI)
        {
            this_dmg *= 3;
            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("110"));
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("111") != null
        && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("111").GetBuffLevel() == SkillLevel.LEGENDARY)
        {
            this_dmg = BuffEffects.buff_legend_111(this_dmg);
            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("110"));
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
