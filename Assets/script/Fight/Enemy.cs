using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum ActionType
{
    Defend,
    Attack,
    None,
}
//敌人脚本
public class Enemy : MonoBehaviour
{
    public static Enemy Instance = new Enemy();
    protected Dictionary<string, string> data;//敌人数据表信息

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defText;
    public Text hpText;
    public Image hpImg;
    public Image hpHitImg;
    //public Image defImg;
    //public Image defHitImg;
    public string AtkAction;
    public string DefAction;

    //数值相关(这部分数值存储在txt中
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关
    SkinnedMeshRenderer _meshRenderer;
    //public Animator ani;

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;

    }

    void Start()
    {
        Instance = this;
        //_meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();//好像是获取轮廓？
        //Debug.Log("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        /*        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");       
        ani = enemy.GetComponent<Animator>();//获取动画控件*/

        //Debug.Log(ani);
        type = ActionType.None;

        //加载敌人血条和行动图标
        hpItemObj = UIManager.Instance.CreateEnemyHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();


        //attackTf = actionObj.transform.Find("atk");
        //defendTf = actionObj.transform.Find("def");

        defText = hpItemObj.transform.Find("EnemyDEFText").GetComponent<Text>();//找到组件中的防御力数值
        hpText = hpItemObj.transform.Find("EnemyHPText").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("EnemyHPFill").GetComponent<Image>();//找到血条图标
        hpHitImg = hpItemObj.transform.Find("EnemyHitHPFill").GetComponent<Image>();//找到血条图标


        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        AtkAction = data["EnemyAction-Attack"];
        DefAction = data["EnemyAction-Defend"];


        UpdateHp();
        UpdateDefend();


    }

    //更新敌人血量
    public void UpdateHp()
    {
        if (CurHp > MaxHp)
        {
            CurHp = MaxHp;
        }

        hpText.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;

        StartCoroutine(ChangeEnemyHitFill());
    }

    public IEnumerator ChangeEnemyHitFill()
    {
        while (hpHitImg.fillAmount > hpImg.fillAmount)
        {
            hpHitImg.fillAmount -= 0.005f;
            yield return new WaitForSeconds(0.02f);
        }

        hpHitImg.fillAmount = hpImg.fillAmount;
        yield break;
    }

    //更新敌人防御
    public void UpdateDefend()
    {
        defText.text = Defend.ToString();

    }


    //护甲扣除
    public void DefendDecrease(int val)
    {
        if (Defend >= val)
        {
            Defend -= val;
        }
        else
            Defend = 0;
    }

    //受到真实伤害
    public void ThroughHited(int val)
    {
        //ani.SetBool("isHitted", true);
        Invoke("SetisHittedToFalse", 0.3f);

        int this_dmg = val;

        CurHp -= this_dmg;
        CheckIfDie();
    }

    //受伤
    public void Hited(int val)
    {
        //ani.SetBool("isHitted", true);
        Invoke("SetisHittedToFalse", 0.3f);

        int this_dmg = val;
   
        //先扣护盾
        Defend -= this_dmg;
        UpdateDefend();

        if (Defend < 0)  //再扣血量
        {
            this_dmg = -Defend;
            Defend = 0;
            UpdateDefend();
            CurHp -= this_dmg;       
        }

        CheckIfDie();
    }

    private void CheckIfDie()
    {
        if (CurHp <= 0)
        {

                CurHp = 0;


                //敌人从列表中移除
                EnemyManager.Instance.DeleteEnemy(this);

                //删除敌人的模型
                Destroy(gameObject);
                Destroy(actionObj);
                Destroy(hpItemObj);
            

        }
        else
        {
            //ani.SetBool("isHitted", true);
        }

        //刷新血量等UI
        UpdateDefend();
        UpdateHp();
    }


    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    public IEnumerator DoAction()
    {
        if (LevelManager.Instance)
        {
            switch (LevelManager.Instance.level)
            {

                case 1:
                    yield return EnemySkill.Instance.EnemyActio0(this, type);
                    break;
                case 2:
                    yield return EnemySkill.Instance.EnemyActio1(this, type);
                    break;
                case 3:
                    yield return EnemySkill.Instance.EnemyActio2(this, type);
                    break;
                case 4:
                    yield return EnemySkill.Instance.EnemyActio3(this, type);
                    break;
                case 5:
                    yield return EnemySkill.Instance.EnemyActio4(this, type);
                    break;
                case 6:
                    yield return EnemySkill.Instance.EnemyActio5(this, type);
                    break;
                case 7:
                    yield return EnemySkill.Instance.EnemyActio6(this, type);
                    break;
                case 8:
                    yield return EnemySkill.Instance.EnemyActio7(this, type);
                    break;
                case 9:
                    yield return EnemySkill.Instance.EnemyActio8(this, type);
                    break;
                case 10:
                    yield return EnemySkill.Instance.EnemyActio9(this, type);
                    break;
                case 11:
                    yield return EnemySkill.Instance.EnemyActio10(this, type);
                    break;
                case 12:
                    switch (RoleManager.Instance.PlayerProfession)
                    {
                        case Professions.PALADIN:
                            yield return EnemySkill.Instance.EnemyActio_PALADIN(this, type);
                            break;
                        case Professions.MONK:
                            yield return EnemySkill.Instance.EnemyActio_MONK(this, type);
                            break;
                        case Professions.SAMURAI:
                            yield return EnemySkill.Instance.EnemyActio_SAMURAI(this, type);
                            break;

                    }
                    break;

            }
        }
        else
        {
            yield return EnemySkill.Instance.EnemyActio0(this, type);
        }
    }


    //随机设定一个敌人行动
    public void SetRandomAction()
    {
        int ran = Random.Range(0, 2);

        type = (ActionType)ran;
    }
}
