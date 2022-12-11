using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemySkill : MonoBehaviour
{
    public static EnemySkill Instance = new EnemySkill();
    
    private int boss1_HpLowerThan10TurnCount = 0;

    //��ͷ��
    public IEnumerator EnemyActio0(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        //GameObject enemy = GameObject.Find("EnemyWaiting"+(LevelManager.Instance.level).ToString()+"(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
    }

    //��
    public IEnumerator EnemyActio1(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("�ظ�");
                enemyInstance.CurHp += 8;
                enemyInstance.UpdateHp();
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
    }

    //��
    public IEnumerator EnemyActio2(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                enemyInstance.CurHp += 4;
                enemyInstance.UpdateHp();
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                FightManager.Instance.GetPlayHit(enemyInstance.Attack-4);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //����
    public IEnumerator EnemyActio3(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                enemyInstance.Defend += 10;
                AudioManager.Instance.PlayEffect("����");
                enemyInstance.UpdateDefend();
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
    }

    //��
    public IEnumerator EnemyActio4(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                FightManager.Instance.GetPlayHit(enemyInstance.Attack-6);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //��
    public IEnumerator EnemyActio5(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //�󲻵�
    public IEnumerator EnemyActio6(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //����³
    public IEnumerator EnemyActio7(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("�ظ�");
                
                System.Random ran = new System.Random();
                int num = ran.Next(100);

                if (num > 50)
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack-7);
                    enemyInstance.CurHp += 7;
                    enemyInstance.UpdateHp();
                }
                else
                {
                    enemyInstance.CurHp += 14;
                    enemyInstance.UpdateHp();
                }
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
    }

    //Թ����
    public IEnumerator EnemyActio8(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                enemyInstance.CurHp -= 14;
                enemyInstance.UpdateHp();
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //������(�ȴ�����ӿ�����
    public IEnumerator EnemyActio9(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        if (RoleManager.Instance.GetTreasure(1) != null)
        {
            RoleManager.Instance.GetTreasure(1).SetPreTime(999);
        }
        if (RoleManager.Instance.GetTreasure(2) != null)
        {
            RoleManager.Instance.GetTreasure(2).SetPreTime(999);
        }
        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //������
    public IEnumerator EnemyActio10(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

/*        GameObject PlayerHpBar = GameObject.Find("playerHPBar");
        PlayerHpBar.SetActive(false);*/

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
    }

    //��
    public IEnumerator EnemyActio_MONK(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level+1).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");

                System.Random ran = new System.Random();
                int num = ran.Next(100);
                if (enemyInstance.CurHp >=150)
                {
                    if (num > 50)
                    {
                        enemyInstance.CurHp += 18;
                        enemyInstance.UpdateHp();
                    }
                    else
                    {
                        enemyInstance.CurHp += 18;
                        enemyInstance.UpdateHp();
                        FightManager.Instance.CurHP += 18;
                        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
                    }
                }
                else
                {
                    FightManager.Instance.GetPlayHit(FightManager.Instance.DefCount);
                    FightManager.Instance.CurHP = FightManager.Instance.MaxHP + LevelManager.Instance.MaxHpFix * 10;
                    UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
                }

                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                System.Random ranA = new System.Random();
                int numA = ranA.Next(100);
                if (numA > 50)
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack - 9);
                    enemyInstance.CurHp += 9;
                    enemyInstance.UpdateHp();
                }
                else
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                }
                
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
    }

    //��ʹ
    public IEnumerator EnemyActio_PALADIN(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaitingP"  + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");

                System.Random ran = new System.Random();
                int num = ran.Next(100);

                    if (num > 50)
                    {
                        enemyInstance.CurHp += 18;
                        enemyInstance.UpdateHp();
                    }
                    else
                    {
                        enemyInstance.CurHp += 9;
                        enemyInstance.UpdateHp();
                        FightManager.Instance.GetPlayHit(9);
                    }


                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                System.Random ranA = new System.Random();
                int numA = ranA.Next(100);
                if (numA > 50)
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack - 9);
                    enemyInstance.CurHp += 9;
                    enemyInstance.UpdateHp();
                }
                else
                {
                    FightManager.Instance.GetPlayHit(FightManager.Instance.DefCount/2);
                }

                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
    }

    //��ͽ
    public IEnumerator EnemyActio_SAMURAI(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting" + (LevelManager.Instance.level+2).ToString() + "(Clone)");
        //ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");

                System.Random ran = new System.Random();
                int num = ran.Next(100);

                if (num > 50)
                {
                    enemyInstance.CurHp += 27;
                    enemyInstance.UpdateHp();
                }
                else
                {
                    enemyInstance.CurHp += 12;
                    enemyInstance.UpdateHp();
                    FightManager.Instance.GetPlayHit(12);
                }


                break;

            case ActionType.Attack:
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("���ʳɹ�2");
                //���������
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //��ҿ�Ѫ
                System.Random ranA = new System.Random();
                int numA = ranA.Next(100);
                if (numA > 40)
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack + 9);
                }
                else
                {
                    FightManager.Instance.GetPlayHit(FightManager.Instance.CurHP / 2);
                }

                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);
        HideEnemyActionText();
        //enemyInstance.HideAction();
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
