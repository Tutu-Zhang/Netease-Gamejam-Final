using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս������ö��
public enum FightType
{
    None,
    Init,
    Player,//��һغ�
    Enemy,//���˻غ�
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


    public int DefCount;//����
    public int TurnCount;//�غ���,����غ�����ÿ����һغϼ�һ�ε�



    private void Start()
    {
        turnBtn = GameObject.Find("turnBtn");
    }

    public void Init()//�����ʼ����
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

    //�л�ս������
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

    //��������߼�
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


        //���ȿۻ���
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
                //ɾ������ģ��
                GameObject enemyModel = GameObject.FindGameObjectWithTag("Enemy");  
                Debug.Log("�ҵ�����ģ��");
                Destroy(enemyModel);
                
                //�л�����Ϸʧ��״̬
                ChangeType(FightType.Lose);
            }
        }

        //���½���
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetRecover(int recover)
    {
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001") != null 
            && UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001").GetBuffLevel() == SkillLevel.LEGENDARY)
        { recover *= 2; }

        AudioManager.Instance.PlayEffect("�ظ�");
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
            AudioManager.Instance.PlayEffect("��ҹ���");
            return;
        }

            AudioManager.Instance.PlayEffect("����");
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

    //�۳����˻���
    public void ArmorBreak(int val)
    {
        AudioManager.Instance.PlayEffect("�Ƽ�");

        EnemyManager.Instance.GetEnemy(0).DefendDecrease(val);
    }

    public void Attack_Enemy(int val)
    {
        AudioManager.Instance.PlayEffect("��ҹ���");
        Debug.Log("AttackEnemyִ��");

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
