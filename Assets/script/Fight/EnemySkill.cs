using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemySkill : MonoBehaviour
{
    public static EnemySkill Instance = new EnemySkill();
    //�����ж�����
    public Animator ani;
    
    private int boss1_HpLowerThan10TurnCount = 0;

    //������Ȯ��û���κ����⼼��
    public IEnumerator EnemyActio0(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 5 + LevelManager.Instance.DefFix;
                AudioManager.Instance.PlayEffect("����");
                enemyInstance.UpdateDefend();
                break;

            case ActionType.Attack:
                Debug.Log("�����ж�");
                ani.SetBool("isAttacking", true);
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                ani.SetBool("isAttacking", false);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    
    //��С������Ѫ������10ʱ��ÿ�غ�����˺���2
    public IEnumerator EnemyActio1(Enemy enemyInstance, ActionType typeIn,int EnemyHP)
    {
        
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();
        int HpLowerThan15 = 0;
        

        
        if (EnemyHP < 15)
        {
            HpLowerThan15 = 1;
            boss1_HpLowerThan10TurnCount += 1;
        }
        else
        {
            HpLowerThan15 = 0;
        }

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        ShowEnemyActionText(enemyInstance, typeIn);

        switch (typeIn)
        {

            case ActionType.None:
                break;
            
            case ActionType.Defend:
                ani.SetBool("isDefending", true);
                AudioManager.Instance.PlayEffect("����");
                yield return new WaitForSeconds(1);

                enemyInstance.Defend += 7 + LevelManager.Instance.DefFix;
                enemyInstance.UpdateDefend();

                ani.SetBool("isDefending", false);
                break;
            
            case ActionType.Attack:
                Debug.Log("�����ж�");
                ani.SetBool("isAttacking", true);
                AudioManager.Instance.PlayEffect("ǹ�繥��");
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack+ boss1_HpLowerThan10TurnCount*2*HpLowerThan15);
                ani.SetBool("isAttacking", false);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        HideEnemyActionText();
        enemyInstance.HideAction();

    }

    //ȭͷ�磺����Ѫ���ϸߣ�defend����ظ�Ѫ����attack��������˺����ظ�Ѫ��,������ÿ�غϼ�1
    public IEnumerator EnemyActio2(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        ShowEnemyActionText(enemyInstance, typeIn);

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.CurHp += 10;
                enemyInstance.Defend += LevelManager.Instance.DefFix;
                AudioManager.Instance.PlayEffect("�ظ�");
                enemyInstance.UpdateHp();
                enemyInstance.UpdateDefend();
                break;

            case ActionType.Attack:
                Debug.Log("�����ж�");
                ani.SetBool("isAttacking", true);                
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                AudioManager.Instance.PlayEffect("ȭͷ����");
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack + FightManager.Instance.TurnCount);
                ani.SetBool("isAttacking", false);

                enemyInstance.CurHp += 5;
                enemyInstance.Defend += LevelManager.Instance.DefFix;
                enemyInstance.UpdateHp();
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        HideEnemyActionText();
        enemyInstance.HideAction();
    }

    //���磺�����������ϸߣ����ܷ�Ϊ���֣�defend��Ӧ����˺����ָ����ף�attack��Ӧ��ɴ����˺��������Ѫ������15��նɱ
    public IEnumerator EnemyActio3(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        ShowEnemyActionText(enemyInstance, typeIn);

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 10 + LevelManager.Instance.DefFix;
                AudioManager.Instance.PlayEffect("����");
                enemyInstance.UpdateDefend();
                yield return new WaitForSeconds(1f);

                ani.SetBool("isAttacking", true);

                AudioManager.Instance.PlayEffect("���繥��");
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(0.5f);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack + 2);
                ani.SetBool("isAttacking", false);
                break;

            case ActionType.Attack:
                ani.SetBool("isAttacking", true);
                AudioManager.Instance.PlayEffect("���繥��1");
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(0.5f);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                
                //��ҿ�Ѫ
                if (FightManager.Instance.CurHP >= 15)
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack + 5);
                }
                else
                {
                    FightManager.Instance.GetPlayHit(FightManager.Instance.CurHP);
                }
                                            
                ani.SetBool("isAttacking", false);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        HideEnemyActionText();
        enemyInstance.HideAction();
    }

    //��ʹ������ʹ����ֵ��Ϊ0����ɹ̶��˺���������ֵΪ0�������Ѫ��>1������Ҵ�1��Ѫ�������Ѫ��=1������ɹ̶��˺�����ɱ���
    //����ʹ����ֵ��Ϊ0ʱ�����100�㻤�ף������»غϻָ�20����ֵ20����ֵ����ʹ��4������Ҳ������5�����ǲ�����
    public IEnumerator EnemyActio4(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ���˶����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        GameObject fire1 = GameObject.Find("/Canvas/��ʹ����/���ڴ���");
        Animator fire1Ani = fire1.GetComponent<Animator>();
        Debug.Log(fire1Ani.name);

        GameObject fire2 = GameObject.Find("/Canvas/��ʹ����/���ڴ���");
        Animator fire2Ani = fire2.GetComponent<Animator>();
        Debug.Log(fire2Ani.name);

        System.Random random = new System.Random();
        double temp = random.NextDouble();

        if (temp >0.2)
        {
            typeIn = ActionType.Attack;
        }
        else
        {
            typeIn = ActionType.Defend;
        }

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        //��ʾ������Ϊ ��ִ�У�
        ShowEnemyActionText(enemyInstance, typeIn);

        Debug.Log(enemyInstance.ifLv4BossConsumeLives);
        //�ж��Ƿ��������� ��ִ�У�
        if (enemyInstance.ifLv4BossConsumeLives)
        {
            Debug.Log("�ж���ʹ�Ƿ���������");
            enemyInstance.Defend -= 80;
            if (enemyInstance.Defend < 20)
            {
                enemyInstance.Defend = 20;
            }
            enemyInstance.CurHp += enemyInstance.MaxHp;
            enemyInstance.UpdateDefend();
            enemyInstance.UpdateHp();

            enemyInstance.ifLv4BossConsumeLives = false;
            typeIn = ActionType.None;
            yield return new WaitForSeconds(1);
        }

        switch (typeIn)
        {
            case ActionType.None:

                break;
            case ActionType.Defend:
                enemyInstance.Defend += 5 + LevelManager.Instance.DefFix;
                enemyInstance.CurHp += 5;

                AudioManager.Instance.PlayEffect("����");
                enemyInstance.UpdateDefend();
                enemyInstance.UpdateHp();
                break;

            case ActionType.Attack:
                yield return new WaitForSeconds(1);
                

                if (enemyInstance.Defend > 0)
                {
                    fire1.SetActive(true);
                    fire1Ani.SetBool("isAttacking", true);
                    AudioManager.Instance.PlayEffect("��ʹ����");
                    yield return new WaitForSeconds(1.2f);
                    //���������
                    Camera.main.DOShakePosition(0.1f, 1.2f, 5, 45);
                    //��ҿ�Ѫ
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                    fire1.SetActive(false);
                }
                else
                {
                    if (FightManager.Instance.CurHP > 1)
                    {
                        int ran = Random.Range(0, 1);

                        if (ran > 0.2)
                        {
                            fire2.SetActive(true);
                            fire2Ani.SetBool("isAttacking", true);
                            AudioManager.Instance.PlayEffect("��ʹ�󼤹�");
                            yield return new WaitForSeconds(1f);
                            //���������
                            Camera.main.DOShakePosition(0.1f, 1.5f, 5, 45);
                            FightManager.Instance.GetPlayHit(FightManager.Instance.CurHP + FightManager.Instance.DefCount - 1);
                            fire2.SetActive(false);
                        }
                        else
                        {
                            fire2.SetActive(true);
                            fire2Ani.SetBool("isAttacking", true);
                            yield return new WaitForSeconds(1f);
                            //���������
                            Camera.main.DOShakePosition(0.1f, 1.5f, 5, 45);
                            FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                            fire2.SetActive(false);
                        }
                    }
                    else
                    {
                        fire2.SetActive(true);
                        fire2Ani.SetBool("isAttacking", true);
                        AudioManager.Instance.PlayEffect("��ʹ�󼤹�");
                        yield return new WaitForSeconds(1f);
                        //���������
                        Camera.main.DOShakePosition(0.1f, 1.5f, 5, 45);
                        FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                        fire2.SetActive(false);
                    }
                }
                break;
        }

        HideEnemyActionText();
    }

    public void ShowEnemyActionText(Enemy enemyInstance,ActionType TypeIn)
    {
        GameObject EnemyActionTextObj = GameObject.FindGameObjectWithTag("EnemyActionText");
        string ActionString = "";
        switch (TypeIn)
        {
            case ActionType.Defend:
                ActionString = enemyInstance.DefAction;
                break;
            case ActionType.Attack:
                ActionString = enemyInstance.AtkAction;
                break;
            case ActionType.None:
                break;
        }
        Debug.Log(EnemyActionTextObj);

        EnemyActionTextObj.GetComponent<Text>().text = ActionString;
    }

    public void HideEnemyActionText()
    {
        GameObject EnemyActionTextObj = GameObject.FindGameObjectWithTag("EnemyActionText");
        EnemyActionTextObj.GetComponent<Text>().text = " ";
    }


}
