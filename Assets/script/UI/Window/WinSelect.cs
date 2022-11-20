using System;
using System.Collections.Specialized;//�ֵ��б�
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSelect : MonoBehaviour
{
    public Button GoToNext;
    //������¼���ѡ��ļ���ӳ��
    public string FinalKey;
    public SkillLevel FinalSkillLevel;
    //����¼����ְҵ����ӳ��
    public int PWhichWasSelect = 0;//��¼�������ѡ������һ�� 
    public string FinalPkey;
    public SkillLevel FinalPSkillLevel;

    // Start is called before the first frame update
    void Start()
    {
        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();

        GoToNext.onClick.AddListener(gotoNext);
        //������

        //�ӿ���Ч������ѡ��3��Ч��������ʾ
        SelectThreeCardSkills();

        //ΪְҵЧ����ť��ӵ���¼�
        AddListenerToSetPCardSkillLevel();
        //Ϊ����ְҵ������ӵ���¼�
        AddListenerToPlayerSkillLevelUp();
        //Ϊ����Ѫ����ӵ���¼�
        AddListenerToMaxHpUp();

        
            Debug.Log(RoleManager.Instance.GetProfession());
    }

    //�ö��������ѡ��
    public void gotoNext()
    {
        RoleManager.Instance.UnlockCardLevel(FinalKey, FinalSkillLevel);
        switch (PWhichWasSelect)    
        {
            case 1://����һ��ְҵЧ��
                RoleManager.Instance.UnlockCardLevel(FinalPkey, FinalPSkillLevel);
                break;
            case 2://ѡ���������ܵȼ�
                PlayerSkillLevelUp();
                break;
            case 3:
                MaxHpUp();
                break;
        }
        SceneManager.LoadScene("selectCardSkills");

    }

    //����3������Ч��
    public void SelectThreeCardSkills()
    {
        List<string> keyList = new List<string>();//����������
        List<SkillLevel> skillLevelList = new List<SkillLevel>();//��ŵȼ����

        //���ȱ�������Ч�����ҳ�����δ��������Ч�����������list
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//����i��Ӧ�������
            for (int j = 0; j < 5; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//����j��Ӧ���ܵȼ�

                if (!RoleManager.Instance.GetCardStatus(key, Clevel))//���δ����
                {
                    keyList.Add(key);
                    skillLevelList.Add(Clevel);
                }
            }
        }


        GameObject obj;
        System.Random rnd = new System.Random();
        for (int i = 1; i <= 3; i++)
        {
            //��Keylist�������ѡһ��
            //�ı�Ч����
            int num = rnd.Next(keyList.Count-1);
            //int num = 20;   
            obj = GameObject.Find("Ч����" + i.ToString());
            int x = DescriptionManager.Instance.PairToNum[keyList[num]];//000��Ӧ������0
            int y = DescriptionManager.Instance.SkillLevelToNum[skillLevelList[num]];//rare��Ӧ2
            obj.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");//��Ҫһ��excel
            //�ı�Ч��ͼ��
            obj = GameObject.Find("Ч��ͼ��" + i.ToString());
            string imgPath = "UI-img/Treasure/�������";
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.GetComponent<Image>().sprite = sp;
            //����Ч��
            obj = GameObject.Find("Ч��" + i.ToString());
            //��ǰ�����ȡ��
            string TempFinalKey = keyList[num];
            SkillLevel TempFinalSkillLevel = skillLevelList[num];
            obj.GetComponent<Button>().onClick.AddListener(() => {
                //RoleManager.Instance.SetCardLevel(keyList[i], skillLevelList[i]);     ����������ֱ�ӽ���
                //�ڵ����һҳʱ�Ž����������
                FinalKey = TempFinalKey;
                FinalSkillLevel = TempFinalSkillLevel;
            });
            Debug.Log(num);
            Debug.Log("�ȼ�Ϊ" + skillLevelList[num]);
            //���ѽ�����Ч���Ƴ�
            keyList.RemoveAt(num);

        }

    }

    //ΪְҵЧ����ť��ӵ���¼� PWhichWasSelect=1
    public void AddListenerToSetPCardSkillLevel()
    {
        //��Բ�ְͬҵ��Ӳ�ͬ�¼�
        switch (RoleManager.Instance.PlayerProfession)
        {
            case Professions.PALADIN:
                SelectAndGetPCardSkillLEvel(SkillLevel.PALADIN);
                break;
            case Professions.MONK:
                SelectAndGetPCardSkillLEvel(SkillLevel.MONK);
                break;
            case Professions.SAMURAI:
                SelectAndGetPCardSkillLEvel(SkillLevel.SAMURAI);
                break;
        }
    }
    public void SelectAndGetPCardSkillLEvel(SkillLevel pro)
    {
        List<string> keyList = new List<string>();//���������ϡ���Ҫ���⴦�����0�����
        List<SkillLevel> skillLevelList = new List<SkillLevel>();//��ŵȼ����
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//����i��Ӧ�������
            for (int j = 0; j < 8; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//����j��Ӧ���ܵȼ�

                if (!RoleManager.Instance.GetCardStatus(key, Clevel) && Clevel == pro && RoleManager.Instance.GetProCardStatus(key, Clevel))//���δ�����������ض�ְҵ
                {
                    keyList.Add(key);
                    skillLevelList.Add(Clevel);
                }
            }
        }


        if (keyList.Count == 0)//����ְҵ���Ƽ���ȫ������
        {
            GameObject obj = GameObject.Find("ְҵЧ��");//��ְҵЧ������
            obj.SetActive(false);
        }
        else//���п��Ƽ�����δ����
        {
            GameObject obj;
            System.Random rnd = new System.Random();
            int num = rnd.Next(keyList.Count-1);
            obj = GameObject.Find("ְҵЧ������");
            int x = DescriptionManager.Instance.PairToNum[keyList[num]];//000��Ӧ������0
            int y = DescriptionManager.Instance.SkillLevelToNum[skillLevelList[num]];//rare��Ӧ2
            obj.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");//��Ҫһ��excel
            //�ı�Ч��ͼ��
            obj = GameObject.Find("ְҵЧ��ͼ��");
            string imgPath = "UI-img/Treasure/�������";
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.GetComponent<Image>().sprite = sp;
            //����Ч��
            obj = GameObject.Find("ְҵЧ��");
            string TempFinalKey = keyList[num];
            SkillLevel TempFinalSkillLevel = skillLevelList[num];
            obj.GetComponent<Button>().onClick.AddListener(() => {
                //RoleManager.Instance.SetCardLevel(keyList[i], skillLevelList[i]);     ����������ֱ�ӽ���
                //�ڵ����һҳʱ�Ž����������
                FinalPkey = TempFinalKey;
                FinalPSkillLevel = TempFinalSkillLevel;
                //���ѡ���˵�һ��
                PWhichWasSelect = 1;
            });
        }
    }

    //Ϊ�������ܰ�ť��ӵ���¼�
    public void AddListenerToPlayerSkillLevelUp()
    {
        //��ȡ�������ܰ�ť
        GameObject obj;
        obj = GameObject.Find("��������");
        obj.GetComponent<Button>().onClick.AddListener(() => {
            PWhichWasSelect = 2;
        });
        //���ְҵ�����Ѿ��ﵽ��˵������ô�ͽ����ѡ������
        if (RoleManager.Instance.ProfessionSkillLevel == SkillLevel.LEGENDARY)
        {
            obj.SetActive(false);
        }
    }
    public void PlayerSkillLevelUp()
    {
        GameObject obj;
        obj = GameObject.Find("��������");
        switch (RoleManager.Instance.ProfessionSkillLevel)
        {
            case SkillLevel.NORMAL:

                    RoleManager.Instance.ProfessionSkillLevel = SkillLevel.RARE;

                break;
            case SkillLevel.RARE:

                    RoleManager.Instance.ProfessionSkillLevel = SkillLevel.EPIC;

                break;
            case SkillLevel.EPIC:

                    RoleManager.Instance.ProfessionSkillLevel = SkillLevel.LEGENDARY;

                break;
            case SkillLevel.LEGENDARY:
                break;
        }
    }

    //Ϊ����Ѫ��������ӵ���¼�
    public void AddListenerToMaxHpUp()
    {
        GameObject obj;
        obj = GameObject.Find("����Ѫ��");
        obj.GetComponent<Button>().onClick.AddListener(() => {
            PWhichWasSelect = 3;
        });
    }
    public void MaxHpUp()//������Ѫ���Ĵ�������LevelManager����
    {
        LevelManager.Instance.MaxHpFix += 1;
    }

}
