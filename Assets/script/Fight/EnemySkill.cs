using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemySkill : MonoBehaviour
{
    public static EnemySkill Instance = new EnemySkill();
    //敌人行动类型
    public Animator ani;
    
    private int boss1_HpLowerThan10TurnCount = 0;

    //大头鲸
    public IEnumerator EnemyActio0(Enemy enemyInstance, ActionType typeIn)
    {
        //获取动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();
        ShowEnemyActionText(enemyInstance, typeIn);
        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("概率成功2");
                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;

            case ActionType.Attack:
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                AudioManager.Instance.PlayEffect("概率成功2");
                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    
    //打交小兵：在血量低于10时，每回合造成伤害加2
    public IEnumerator EnemyActio1(Enemy enemyInstance, ActionType typeIn,int EnemyHP)
    {
        
        //获取动画控件
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

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        ShowEnemyActionText(enemyInstance, typeIn);

        switch (typeIn)
        {

            case ActionType.None:
                break;
            
            case ActionType.Defend:
                ani.SetBool("isDefending", true);
                AudioManager.Instance.PlayEffect("护甲");
                yield return new WaitForSeconds(1);

                enemyInstance.Defend += 7 + LevelManager.Instance.DefFix;
                enemyInstance.UpdateDefend();

                ani.SetBool("isDefending", false);
                break;
            
            case ActionType.Attack:
                Debug.Log("敌人行动");
                ani.SetBool("isAttacking", true);
                AudioManager.Instance.PlayEffect("枪哥攻击");
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack+ boss1_HpLowerThan10TurnCount*2*HpLowerThan15);
                ani.SetBool("isAttacking", false);
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        HideEnemyActionText();
        enemyInstance.HideAction();

    }

    //拳头哥：基础血量较高，defend代表回复血量，attack代表造成伤害并回复血量,攻击力每回合加1
    public IEnumerator EnemyActio2(Enemy enemyInstance, ActionType typeIn)
    {
        //获取动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        ShowEnemyActionText(enemyInstance, typeIn);

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.CurHp += 10;
                enemyInstance.Defend += LevelManager.Instance.DefFix;
                AudioManager.Instance.PlayEffect("回复");
                enemyInstance.UpdateHp();
                enemyInstance.UpdateDefend();
                break;

            case ActionType.Attack:
                Debug.Log("敌人行动");
                ani.SetBool("isAttacking", true);                
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                AudioManager.Instance.PlayEffect("拳头攻击");
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack + FightManager.Instance.TurnCount);
                ani.SetBool("isAttacking", false);

                enemyInstance.CurHp += 5;
                enemyInstance.Defend += LevelManager.Instance.DefFix;
                enemyInstance.UpdateHp();
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        HideEnemyActionText();
        enemyInstance.HideAction();
    }

    //刀哥：基础攻击面板较高，技能分为两种，defend对应造成伤害并恢复护甲，attack对应造成大量伤害，若玩家血量低于15则斩杀
    public IEnumerator EnemyActio3(Enemy enemyInstance, ActionType typeIn)
    {
        //获取动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        ShowEnemyActionText(enemyInstance, typeIn);

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 10 + LevelManager.Instance.DefFix;
                AudioManager.Instance.PlayEffect("护甲");
                enemyInstance.UpdateDefend();
                yield return new WaitForSeconds(1f);

                ani.SetBool("isAttacking", true);

                AudioManager.Instance.PlayEffect("刀哥攻击");
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(0.5f);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack + 2);
                ani.SetBool("isAttacking", false);
                break;

            case ActionType.Attack:
                ani.SetBool("isAttacking", true);
                AudioManager.Instance.PlayEffect("刀哥攻击1");
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(0.5f);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                
                //玩家扣血
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

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        HideEnemyActionText();
        enemyInstance.HideAction();
    }

    //天使：若天使护甲值不为0，造成固定伤害；若护甲值为0，且玩家血量>1，则将玩家打到1点血；若玩家血量=1，则造成固定伤害（击杀玩家
    //当天使生命值降为0时，获得100点护甲，并在下回合恢复20生命值20护甲值。天使有4条命（也可能是5条，记不清了
    

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
