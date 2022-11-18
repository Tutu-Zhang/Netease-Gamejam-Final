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
//���˽ű�
public class Enemy : MonoBehaviour
{
    public static Enemy Instance = new Enemy();
    protected Dictionary<string, string> data;//�������ݱ���Ϣ

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI���
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

    //��ֵ���(�ⲿ����ֵ�洢��txt��
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;
    public int Lv4BossLives;
    public bool ifLv4BossConsumeLives;

    //������
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
        Lv4BossLives = 2;
        ifLv4BossConsumeLives = false;
    }

    void Start()
    {
        Instance = this;
        //_meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();//�����ǻ�ȡ������
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");       
        ani = enemy.GetComponent<Animator>();//��ȡ�����ؼ�

        Debug.Log(ani);
        type = ActionType.None;

        //���ص���Ѫ�����ж�ͼ��
        hpItemObj = UIManager.Instance.CreateEnemyHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();


        attackTf = actionObj.transform.Find("atk");
        defendTf = actionObj.transform.Find("def");

        defText = hpItemObj.transform.Find("EnemyDEFText").GetComponent<Text>();//�ҵ�����еķ�������ֵ
        hpText = hpItemObj.transform.Find("EnemyHPText").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("EnemyHPFill").GetComponent<Image>();//�ҵ�Ѫ��ͼ��
        hpHitImg = hpItemObj.transform.Find("EnemyHitHPFill").GetComponent<Image>();//�ҵ�Ѫ��ͼ��


        //��ʼ����ֵ
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        AtkAction = data["EnemyAction-Attack"];
        DefAction = data["EnemyAction-Defend"];


        UpdateHp();
        UpdateDefend();


    }

    //���µ���Ѫ��
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

    //���µ��˷���
    public void UpdateDefend()
    {
        defText.text = Defend.ToString();

    }

    //���˱�ѡ��ʱ��ʾ���
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("OtlColor", Color.red);//���������һ������ɫ����
    }

    //δѡ�е���ʱ���˵���ɫ
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("OtlColor", Color.black);//���������һ������ɫ����
    }

    //���׿۳�
    public void DefendDecrease(int val)
    {
        if (Defend >= val)
        {
            Defend -= val;
        }
        else
            Defend = 0;
    }

    //�ܵ���ʵ�˺�
    public void ThroughHited(int val)
    {
        ani.SetBool("isHitted", true);
        Invoke("SetisHittedToFalse", 0.3f);

        int this_dmg = val;

        CurHp -= this_dmg;
        CheckIfDie();
    }

    //����
    public void Hited(int val)
    {
        ani.SetBool("isHitted", true);
        Invoke("SetisHittedToFalse", 0.3f);

        int this_dmg = val;
   
        //�ȿۻ���
        Defend -= this_dmg;
        UpdateDefend();

        if (Defend < 0)  //�ٿ�Ѫ��
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
            //���Ĺ�boss��������
            if (LevelManager.Instance.level == 4 && Lv4BossLives > 0)
            {
                CurHp = 1;
                Defend += 100;
                AudioManager.Instance.PlayEffect("����");
                Lv4BossLives -= 1;

                UpdateHp();
                UpdateDefend();
                ifLv4BossConsumeLives = true;
            }
            else
            {
                CurHp = 0;


                //���˴��б����Ƴ�
                EnemyManager.Instance.DeleteEnemy(this);

                //ɾ�����˵�ģ��
                Destroy(gameObject);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }

        }
        else
        {
            ani.SetBool("isHitted", true);
        }

        //ˢ��Ѫ����UI
        UpdateDefend();
        UpdateHp();
    }

    //�����ӳٴ���
    public void SetisHittedToFalse()
    {
        ani.SetBool("isHitted", false);
    }

    //���ع���ͷ�ϵ��ж���־
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

                case 0:
                    yield return EnemySkill.Instance.EnemyActio0(this, type);
                    break;
                case 1:
                    yield return EnemySkill.Instance.EnemyActio1(this, type, CurHp);
                    break;
                case 2:
                    yield return EnemySkill.Instance.EnemyActio2(this, type);
                    break;
                case 3:
                    yield return EnemySkill.Instance.EnemyActio3(this, type);
                    break;
                case 4:
                    yield return EnemySkill.Instance.EnemyActio4(this, type);
                    break;
            }
        }
        else
        {
            yield return EnemySkill.Instance.EnemyActio0(this, type);
        }
    }


    //����趨һ�������ж�
    public void SetRandomAction()
    {
        int ran = Random.Range(0, 2);

        type = (ActionType)ran;

        //type = ActionType.Attack;
    }
}
