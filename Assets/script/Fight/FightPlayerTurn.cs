using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");
        //FightManager.Instance.SetBtn(false);

        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {
            UIManager.Instance.GetUI<FightUI>("fightBackground").refreshBuff();

            //回合前就执行效果的判定阶段
            //BuffItem buff_1 = UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0101");
            if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001") != null)
            {
                BuffEffects.MatchBuff("001", UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("001").GetBuffLevel());
            }

            if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuffWithLvl("010", SkillLevel.LEGENDARY) != null)
            {
                BuffEffects.MatchBuff("010", SkillLevel.LEGENDARY);
            }
            UIManager.Instance.GetUI<FightUI>("fightBackground").BuffPassTurn();
            UIManager.Instance.GetUI<FightUI>("fightBackground").TreasurePassTurn();
            UIManager.Instance.GetUI<FightUI>("fightBackground").SkillUsed = false;

            //抽牌

            int drawCardCount = 6 - UIManager.Instance.GetUI<FightUI>("fightBackground").GetCardNum() - UIManager.Instance.GetUI<FightUI>("fightBackground").GetPlayCardNum() ;
            //Debug.Log(drawCardCount);//已经修改为补满牌
            UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//补满卡牌
            UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();//更新卡牌位置

            FightManager.Instance.SetBtn(true);


        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
