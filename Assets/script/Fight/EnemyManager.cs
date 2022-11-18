using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���˹�����
public class EnemyManager 
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//�洢ս���еĵ���
 
    //���ص�����Դ
    public void loadRes(string id)
    {

        enemyList = new List<Enemy>();
        
        //�����ȡ���ǹؿ���
        /*
        Id	Name	EnemyIds	Pos
        Id	�ؿ�����	����Id������	���й����λ��
        10001	1	10001	"0,0,0"
        10002	2	10001=10001	"0,0,0=0,0,1"
        10003	3	10001=10002=10003	"3,0,1=0,0,1=-3,0,1"
        */
        Dictionary<string, string> LevelData = GameConfigManager.Instance.GetLevelById(id);

        string[] enemyids = LevelData["EnemyIds"].Split('=');

        string[] enemyPos = LevelData["Pos"].Split('=');

        //Debug.Log("������" + enemyids.Length + "������");

        for (int i = 0; i < enemyids.Length; i++)
        {
            string enemyid = enemyids[i];
            string[] posArr = enemyPos[i].Split(',');


            //����id��ȡ�������˵���Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyid);

            Debug.Log("����ģ����ȡ" + enemyData["Model"]);
            
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//����Դ���ض�Ӧ�ĵ���ģ��    

            Enemy enemy = obj.AddComponent<Enemy>();//Ϊ����������ӽű�

            enemy.Init(enemyData);//�洢������Ϣ

            enemyList.Add(enemy);//�洢�������б�

            //��һ�ص���λ������
            //Debug.Log(UIManager.Instance.GetUI<FightUI>("fightBackground").transform.Find("EnemyCase").position);
            obj.transform.position = UIManager.Instance.GetUI<FightUI>("fightBackground").transform.Find("EnemyCase").position;
        }
    }

    //ɾ������
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //�������Ƿ��ɱ���˵��ж�
        if (enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }
    //ִ����Ȼ���Ĺ������Ϊ
    public IEnumerator DoAllEnemyAction()
    {

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("101") != null)
        {
            BuffEffects.MatchBuff("101", UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("101").GetBuffLevel());
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("111") != null)
        {
            BuffEffects.MatchBuff("111", UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("111").GetBuffLevel());
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1100") != null)
        {
            if (BuffEffects.buff_silence_1100())
            {
                FightManager.Instance.ChangeType(FightType.Player);
                yield break;
            }
            
        }

        //�������е�����Ϊͼ��
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //ִ�е�����Ϊ
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }


        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public Enemy GetEnemy(int index)
    {
        return enemyList[index];
    }

}
