using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        //ɾ�����п���(���ǲ���Ҫ��ʾɾ������
        //UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        FightManager.Instance.SetBtn(false); //�ص������غϰ�ť

        //��ʾ���˻غ���ʾ
        UIManager.Instance.ShowTip("���˻غ�", Color.red, delegate ()
        {
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });


    }



    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
