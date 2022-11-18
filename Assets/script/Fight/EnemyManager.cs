using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敌人管理器
public class EnemyManager 
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//存储战斗中的敌人
 
    //加载敌人资源
    public void loadRes(string id)
    {

        enemyList = new List<Enemy>();
        
        //这里读取的是关卡表
        /*
        Id	Name	EnemyIds	Pos
        Id	关卡名称	敌人Id的数组	所有怪物的位置
        10001	1	10001	"0,0,0"
        10002	2	10001=10001	"0,0,0=0,0,1"
        10003	3	10001=10002=10003	"3,0,1=0,0,1=-3,0,1"
        */
        Dictionary<string, string> LevelData = GameConfigManager.Instance.GetLevelById(id);

        string[] enemyids = LevelData["EnemyIds"].Split('=');

        string[] enemyPos = LevelData["Pos"].Split('=');

        //Debug.Log("加载了" + enemyids.Length + "个敌人");

        for (int i = 0; i < enemyids.Length; i++)
        {
            string enemyid = enemyids[i];
            string[] posArr = enemyPos[i].Split(',');


            //根据id获取单个敌人的信息
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyid);

            Debug.Log("敌人模型提取" + enemyData["Model"]);
            
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//从资源加载对应的敌人模型    

            Enemy enemy = obj.AddComponent<Enemy>();//为敌人物体添加脚本

            enemy.Init(enemyData);//存储敌人信息

            enemyList.Add(enemy);//存储到敌人列表

            //第一关敌人位置配置
            //Debug.Log(UIManager.Instance.GetUI<FightUI>("fightBackground").transform.Find("EnemyCase").position);
            obj.transform.position = UIManager.Instance.GetUI<FightUI>("fightBackground").transform.Find("EnemyCase").position;
        }
    }

    //删除敌人
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //后续做是否击杀敌人的判断
        if (enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }
    //执行仍然存活的怪物的行为
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

        //更新所有敌人行为图标
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //执行敌人行为
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }


        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public Enemy GetEnemy(int index)
    {
        return enemyList[index];
    }

}
