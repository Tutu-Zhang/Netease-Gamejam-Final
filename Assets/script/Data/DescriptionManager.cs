using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public static DescriptionManager Instance = new DescriptionManager();

    //描述框
    private GameObject Description;
    //选择你的职业
    private GameObject Profession;

    //从0，1，2到000，001，010
    public Dictionary<int, string> NumToPair;
    public Dictionary<string, int> PairToNum;
    //从0，1，2到NONE，NORMAL,RARE
    public Dictionary<int, SkillLevel> NumToSkillLevel;
    public Dictionary<SkillLevel, int> SkillLevelToNum;

    //从0，1，2到RARE, EPIC, LEGEND（稀有度
    public Dictionary<int, TreasureLevel> NumToTLevel;
    //从0，1，2到BUFF, ROUND, PERGAME（类型
    public Dictionary<int, TreasureCategory> NumToCategory;
    //从0，1，2到GENERAL，PALADIN, MONK, SAMURAI（职业
    public Dictionary<int, TreasurePro> NumToPro;




    void Awake()
    {
        Instance = this;
    }

    //读取并改变职业描述
    public void ChangePerfessionDescription(Professions pro)
    {
        Profession = GameObject.Find("Profession");
        Description = GameObject.Find("Description");
        switch (pro)
        {
            case Professions.PALADIN:               
                Profession.GetComponent<Text>().text = "我将净化所有罪恶！";
                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(0,0, "ProfessionDes");
                break;
            case Professions.MONK:
                Profession.GetComponent<Text>().text = "让神明感受痛苦！";
                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(0,1, "ProfessionDes");
                break;
            case Professions.SAMURAI:
                Profession.GetComponent<Text>().text = "我的我的都是我的！";
                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(0,2, "ProfessionDes");
                break;
        }
    }

    //建立数字到稀有度，职业和数字到组合的字典
    public void CreatlNumToCardSkilDictionary()
    {
        NumToPair = new Dictionary<int, string>
        {
            {0,"000" },
            {1,"001" },
            {2,"010" },
            {3,"011" },
            {4,"100" },
            {5,"101" },
            {6,"110" },
            {7,"111" }
        };

        PairToNum = new Dictionary<string, int>
        {
            {"000",0 },
            {"001",1 },
            {"010",2 },
            {"011",3 },
            {"100",4 },
            {"101",5 },
            {"110",6 },
            {"111",7 }
        };

        NumToSkillLevel = new Dictionary<int, SkillLevel>
        {
            {0,SkillLevel.NONE },
            {1,SkillLevel.NORMAL },
            {2,SkillLevel.RARE },
            {3,SkillLevel.EPIC },
            {4,SkillLevel.LEGENDARY},
            {5,SkillLevel.PALADIN },
            {6,SkillLevel.MONK },
            {7,SkillLevel.SAMURAI }
        };

        SkillLevelToNum = new Dictionary<SkillLevel, int>
        {
            {SkillLevel.NONE, 0},
            {SkillLevel.NORMAL, 1},
            {SkillLevel.RARE, 2},
            {SkillLevel.EPIC, 3},
            {SkillLevel.LEGENDARY,4},
            {SkillLevel.PALADIN, 5},
            {SkillLevel.MONK,6 },
            {SkillLevel.SAMURAI, 7}
        };
    }

    public void CreatNumToTreasureDictionary()
    {
        NumToTLevel = new Dictionary<int, TreasureLevel>
        {
            {0,TreasureLevel.RARE },
            {1,TreasureLevel.EPIC },
            {2,TreasureLevel.LEGEND }
        };

        NumToCategory = new Dictionary<int, TreasureCategory>
        {
            {0,TreasureCategory.BUFF },
            {1,TreasureCategory.ROUND },
            {2,TreasureCategory.PERGAME }
        };

        NumToPro = new Dictionary<int, TreasurePro>
        {
            {0,TreasurePro.GENERAL },
            {1,TreasurePro.PALADIN },
            {2,TreasurePro.MONK },
            {3,TreasurePro.SAMURAI }
        };
    }
}
