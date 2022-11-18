using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌(我们不需要显示删除卡牌
        //UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        FightManager.Instance.SetBtn(false); //关掉结束回合按钮

        //显示敌人回合提示
        UIManager.Instance.ShowTip("敌人回合", Color.red, delegate ()
        {
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });


    }



    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
