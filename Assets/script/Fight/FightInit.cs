using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//卡牌战斗初始化
public class FightInit : FightUnit
{
    //进入该页面时调用
    public void Start()
    {

        //初始化配置表
        GameConfigManager.Instance.Init();

        //初始化战斗数值
        FightManager.Instance.Init();

        //初始化战斗卡牌数据
        FightCardManager.Instance.Init();

        //加载战斗元素
        UIManager.Instance.ShowUI<FightUI>("fightBackground");

        int levelCount = LevelManager.Instance.level;
        Debug.Log(levelCount);

        //int levelCount = 0;

        //加载关卡资源
        switch (levelCount)
        {
            case -1:
                EnemyManager.Instance.loadRes("10000");
                break;

            case 1:
                EnemyManager.Instance.loadRes("10000");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("浅海战斗Infinite Horizons - FiftySounds");
                break;
            case 2:
                EnemyManager.Instance.loadRes("10001");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("浅海战斗Infinite Horizons - FiftySounds");
                break;
            case 3:
                EnemyManager.Instance.loadRes("10002");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("浅海战斗Infinite Horizons - FiftySounds");
                break;
            case 4:
                EnemyManager.Instance.loadRes("10003");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("Boss2满月之潮");
                break;
            case 5:
                EnemyManager.Instance.loadRes("10004");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("中海战斗Unnatural");
                break;
            case 6:
                EnemyManager.Instance.loadRes("10005");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("中海战斗Unnatural");
                break;
            case 7:
                EnemyManager.Instance.loadRes("10006");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("中海战斗Unnatural");
                break;
            case 8:
                EnemyManager.Instance.loadRes("10007");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("Boss2满月之潮");
                break;
            case 9:
                EnemyManager.Instance.loadRes("10008");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("深海战斗Morpheus");
                break;
            case 10:
                EnemyManager.Instance.loadRes("10009");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("深海战斗Morpheus");
                break;
            case 11:
                EnemyManager.Instance.loadRes("100010");
                //播放战斗bgm，这里只需要输入bgm的名字就可以
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("深海战斗Morpheus");
                break;
            case 12:
                switch (RoleManager.Instance.PlayerProfession)
                {
                    case Professions.PALADIN:
                        EnemyManager.Instance.loadRes("100011");
                        //播放战斗bgm，这里只需要输入bgm的名字就可以
                        if (AudioManager.Instance != null)
                            AudioManager.Instance.PlayBGM("Boss2满月之潮");
                        break;
                    case Professions.MONK:
                        EnemyManager.Instance.loadRes("100012");
                        //播放战斗bgm，这里只需要输入bgm的名字就可以
                        if (AudioManager.Instance != null)
                            AudioManager.Instance.PlayBGM("Boss2满月之潮");
                        break;  
                    case Professions.SAMURAI:
                        EnemyManager.Instance.loadRes("100013");
                        //播放战斗bgm，这里只需要输入bgm的名字就可以
                        if (AudioManager.Instance != null)
                            AudioManager.Instance.PlayBGM("Boss2满月之潮");
                        break;
                }
                break;
        }


        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }





    public override void OnUpdate()
    {

    }
}
