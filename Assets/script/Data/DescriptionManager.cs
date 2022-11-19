using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public static DescriptionManager Instance = new DescriptionManager();

    //������
    private GameObject Description;
    //ѡ�����ְҵ
    private GameObject Profession;

    //��0��1��2��000��001��010
    public Dictionary<int, string> NumToPair;
    //��0��1��2��NONE��NORMAL,RARE
    public Dictionary<int, SkillLevel> NumToSkillLevel;

    //��0��1��2��RARE, EPIC, LEGEND��ϡ�ж�
    public Dictionary<int, TreasureLevel> NumToTLevel;
    //��0��1��2��BUFF, ROUND, PERGAME������
    public Dictionary<int, TreasureCategory> NumToCategory;
    //��0��1��2��GENERAL��PALADIN, MONK, SAMURAI��ְҵ
    public Dictionary<int, TreasurePro> NumToPro;




    void Awake()
    {
        Instance = this;
    }

    //��ȡ���ı�ְҵ����
    public void ChangePerfessionDescription(Professions pro)
    {
        Profession = GameObject.Find("Profession");
        Description = GameObject.Find("Description");
        switch (pro)
        {
            case Professions.PALADIN:               
                Profession.GetComponent<Text>().text = "�ҽ������������";
                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(0,0, "ProfessionDes");
                break;
            case Professions.MONK:
                Profession.GetComponent<Text>().text = "����������ʹ�࣡";
                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(0,1, "ProfessionDes");
                break;
            case Professions.SAMURAI:
                Profession.GetComponent<Text>().text = "�ҵ��ҵĶ����ҵģ�";
                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(0,2, "ProfessionDes");
                break;
        }
    }

    //�������ֵ�ϡ�жȣ�ְҵ�����ֵ���ϵ��ֵ�
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
