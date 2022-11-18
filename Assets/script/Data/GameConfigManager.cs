using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    //卡牌表
    private GameConfigData cardData;
    //敌人表
    private GameConfigData enemyData;
    //关卡表
    private GameConfigData levelData;

    private GameConfigData cardTypeData;

    private GameConfigData playerSkill;

    private GameConfigData DialogueBeforeData;

    private GameConfigData DialogueAfterData;

    private GameConfigData BuffDescriptionData;

    private TextAsset textAsset;

    //初始化配置文件（txt 存储到内存中）
    public void Init()
    {
        //读取卡牌，敌人等的数据       
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/playerSkill");
        playerSkill = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/BuffDes");
        BuffDescriptionData = new GameConfigData(textAsset.text);
    }

    public List<Dictionary<string,string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }





    public Dictionary<string,string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }
    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    public Dictionary<string, string> GetCardTypeById(string id)
    {
        return cardTypeData.GetOneById(id);
    }

    public Dictionary<string, string> GetPlayerSkillsById(string id)
    {
        return playerSkill.GetOneById(id);
    }

    public Dictionary<string, string> GetBuffDesById(string id)
    {
        return BuffDescriptionData.GetOneById(id);
    }
}
