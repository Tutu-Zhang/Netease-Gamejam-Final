using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectTreasureUI : MonoBehaviour
{
    public Button GoToNext;
    public GameObject Description;
    private Professions Player_Pro;
    private int selectGTreasure = 0;
    private int selectPTreasure = 0;


    // Start is called before the first frame update
    void Start()
    {
/*        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("SelectUI����BGM");
            AudioManager.Instance.PlayBGM("����BGM");
        }*/
        DescriptionManager.Instance.CreatNumToTreasureDictionary();

        GoToNext.onClick.AddListener(gotoNext);
        GetTreasureUnlockSituration();

    }
    //��ñ���Ľ������
    public void GetTreasureUnlockSituration()
    {
        Description = GameObject.Find("Description");
        Player_Pro = RoleManager.Instance.PlayerProfession;//��ȡ��ǰ���ְҵ

        GameObject obj = new GameObject();

        //�ȴ����ְҵ�����
        for (int i = 0; i < 3; i++)
        {
            TreasureLevel TL = DescriptionManager.Instance.NumToTLevel[i];//i��Ӧ�����꣬����ȼ�
            for (int j = 0; j < 3; j++)
            {
                TreasureCategory TC = DescriptionManager.Instance.NumToCategory[j];//j��Ӧ�����꣬��������
                
                if (RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TL, TC).IfUnlock)//�������
                {
                    obj = GameObject.Find("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    
                    Debug.Log("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                   

                    //��ӵ���¼�,��ӱ��ﵽ��Ϸ����ʾ����
                    int x = i;
                    int y = j;
                    
                    obj.GetComponent<Button>().onClick.AddListener(() => {
                        selectGTreasure = 1;//�����ж�����Ƿ�ѡ������������
                        
                        //����󣬽��ñ�����ڱ���1��λ��
                        RoleManager.Instance.SetTreasure(1, RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TL, TC));
                        Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                        Debug.Log(x + " " + y);
                        //��ʾ����ѡ��Ч��
                        SetTIconSelected(3, 3, x+1, y+1, "general");
                        //�鿴�Ƿ�ɹ��󶨵�1λ��
                        Debug.Log(RoleManager.Instance.GetTreasure(1).TCategory);
                    });
                    
                }
                else//����δ����
                {
                    Debug.Log("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    obj = GameObject.Find("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    //��ӵ���¼�,��ӱ��ﵽ��Ϸ����ʾ����
                    int x = i;
                    int y = j;
                    obj.GetComponent<Button>().onClick.AddListener(() => {


                        Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                    });
                }
                


            }
        }

        Debug.Log(Player_Pro);
        //�ٴ���ְҵ�������ְҵ�������ˣ�ʵ����ֻ��Ҫ������Ҳ���Ǳ������ͣ��Ϳ���ȷ��һ������
        switch (Player_Pro)
        {
            case Professions.PALADIN:
                PTreasureLinkAndDes(obj, TreasurePro.PALADIN, 3);
                break;
            case Professions.MONK:
                PTreasureLinkAndDes(obj, TreasurePro.MONK, 4);
                break;
            case Professions.SAMURAI:
                PTreasureLinkAndDes(obj, TreasurePro.SAMURAI, 5);
                break;
        }   
            
    }


    public void gotoNext()
    {
        if (selectGTreasure + selectPTreasure == 2)
        {
            SceneManager.LoadScene("game1");
        }
        else
        {
            Description = GameObject.Find("Description");
            Description.GetComponent<Text>().text = "ȷ����ѡ������Я���������ｫʹ��ս��������";
        }
        
        
    }

    public void PTreasureLinkAndDes(GameObject obj, TreasurePro treasurePro, int i)//i��ְҵȷ����������excel���ǵڼ���
    {
        for (int j = 0; j < 3; j++)
        {
            TreasureCategory TC = DescriptionManager.Instance.NumToCategory[j];//j��Ӧ�����꣬��������
            if (RoleManager.Instance.GetTreasureStatus(treasurePro, TreasureLevel.LEGEND, TC).IfUnlock)//�������
            {
                obj = GameObject.Find("PTreasure" + "1" + (j + 1).ToString());

                Debug.Log("PTreasure" + "1" + (j + 1).ToString());

                //�ı䱦��ͼ��
                string imgPath = ExcelReader.Instance.GetProfessionDes(i, DescriptionManager.Instance.CategoryToNum[TC], "TreasureIcon"); ;
                Texture2D texture = Resources.Load<Texture2D>(imgPath);
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                obj.GetComponent<Image>().sprite = sp;

                //��ӵ���¼�,��ӱ��ﵽ��Ϸ����ʾ����
                int x = i;//iʼ�յ���4��
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => {
                    selectPTreasure = 1;
                    SetTIconSelected(1, 3, 1, y + 1, "profession");
                    //����󣬽��ñ�����ڱ���2��λ��
                    RoleManager.Instance.SetTreasure(2, RoleManager.Instance.GetTreasureStatus(treasurePro, TreasureLevel.LEGEND, TC));
                    Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                    Debug.Log("����˱���");
                });

            }
            else//����δ����
            {
                Debug.Log("PTreasure" + "1" + (j + 1).ToString());
                obj = GameObject.Find("PTreasure" + "1" + (j + 1).ToString());
                //��ӵ���¼�,��ӱ��ﵽ��Ϸ����ʾ����
                int x = i;
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => {
                    Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                    Debug.Log("����˱���");
                });
            }
        }
    }

    //�ڵ��һ����ť֮����ʾ��ѡ��Ч��������ͬ�������ġ���ѡ������
    public void SetTIconSelected(int groupCountX, int groupCountY, int whichToSelectX,int whichToSelectY, string groupName)//һ���ж�����GroupCount���Լ�Ҫ���õ�����һ��WhichToSelect����Ӧj��groupName����000��001����Ӧkey
    {
        //���������ڵ�������ѡ������
        for (int i = 1; i <= groupCountX; i++)//groupCountX����ְҵ�������3��ְҵ�������1
        {
            for (int j = 1; j <= groupCountY; j++)//groupCountY��ʼ��Ϊ3
            {
                //�ҵ��ض��ġ���ѡ��
                GameObject selectedText = GameObject.Find(groupName + "-" + i.ToString()+ j.ToString());
                Debug.Log("����İ�ť�ǣ�" + groupName + "-" + whichToSelectX.ToString() + whichToSelectY.ToString());

                if (i == whichToSelectX && j == whichToSelectY)//����������������ť����Ҫѡ��������ť
                {

                    Debug.Log(selectedText);
                    selectedText.GetComponent<Text>().text = "��ѡ��";
                }
                else//�������
                {
                    selectedText.GetComponent<Text>().text = " ";
                }
            }



        }
    }

}
