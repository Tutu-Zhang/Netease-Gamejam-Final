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
                    
                    //�ı䱦��ͼ��
                    string imgPath = "UI-img/Treasure/�������";
                    Texture2D texture = Resources.Load<Texture2D>(imgPath);
                    Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    obj.GetComponent<Image>().sprite = sp;

                    //��ӵ���¼�,��ӱ��ﵽ��Ϸ����ʾ����
                    int x = i;
                    int y = j;
                    obj.GetComponent<Button>().onClick.AddListener(() => {
                        //����󣬽��ñ�����ڱ���1��λ��
                        RoleManager.Instance.SetTreasure(1, RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TL, TC));
                        Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
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
        SceneManager.LoadScene("game1");
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
                string imgPath = "UI-img/Treasure/�������";
                Texture2D texture = Resources.Load<Texture2D>(imgPath);
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                obj.GetComponent<Image>().sprite = sp;

                //��ӵ���¼�,��ӱ��ﵽ��Ϸ����ʾ����
                int x = i;
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => {
                    //����󣬽��ñ�����ڱ���2��λ��
                    RoleManager.Instance.SetTreasure(2, RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TreasureLevel.LEGEND, TC));
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

}
