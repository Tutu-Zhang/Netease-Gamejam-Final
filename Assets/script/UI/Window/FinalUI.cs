using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalUI : MonoBehaviour
{
    public GameObject Final;
    public Button BackToBegin;
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("ִ�����ָı�");
        AudioManager.Instance.PlayBGM("���");

        ChangText();
        BackToBegin.onClick.AddListener(ReturnToBegin);

    }

    public void ChangText()
    {

        Final = GameObject.Find("FinalText");
        switch (RoleManager.Instance.PlayerProfession)
        {

            case Professions.PALADIN:
                Final.GetComponent<Text>().text = "�����ۣ���Ư���ں����ϡ�"+ "\n"+"����������Ӱ���٣�֮ǰ������һ������һ���Ρ�"+"\n"+ "����û�о�������ķ�����"+"\n"+ "������й�ĵط�����Ӱ��������Ǵ�ǧ�����һ���֡�"
                    +"\n"+ "�˼䲢���ǷǺڼ��ף��Ȳ����ɵ�����Ҳû��������á�"+"\n"+ "��������������죬����ȥ���Լ���ѧʶȥ���������͹�ȥ���Լ�һ����������̶�е��˾��ꡣ"+"\n"+ "һ��ȥ���´�½�Ĵ����ֲ��������㡣"
                    +"\n"+ "վ�ڼװ��ϣ�����Զ����Ϧ�������ƺ��ҵ��������µķ���" ;
                break;
            case Professions.MONK:
                Final.GetComponent<Text>().text = "�����ۣ���Ư���ں����ϡ�" + "\n" + "����������Ӱ���٣�֮ǰ������һ������һ���Ρ�" + "\n" + "��սʤ����ս��ȴ�е������ޱȡ�" + "\n" + "��˼����Ҳ����Ѳ���ֵ��������ĥ����־����Ϊ���ò���˲������档"
    + "\n" + "����׷�����õ�������ྡ֮��Ҳδ���и�����" + "\n" + "��������Լ�����꣬������ĥ�������������ȥ�Դ����" + "\n" + "һ��ȥ���´�½�Ĵ����ֲ��������㡣"
    + "\n" + "վ�ڼװ��ϣ�����Զ����Ϧ�������ƺ��ҵ��������µķ���";
                break;
            case Professions.SAMURAI:
                Final.GetComponent<Text>().text = "�����ۣ���Ư���ں����ϡ�" + "\n" + "����������Ӱ���٣�֮ǰ������һ������һ���Ρ�" + "\n" + "����û���޾��ı��غ�����Ļ��䡣" + "\n" + "���Ȼ������ʲô��"
    + "\n" + "�����Ǹ��ںӱ�����ĺ�ͯ�������Ǹ����Ž�����Ұ�䱼�ܵ���Ӱ��" + "\n" + "ԭ����������ʵĿ��֣�һֱ�������Լ����С�" + "\n" + "��Щû�б���������ģ����ĸ��飬���������޾����ء�" + "\n"+"һ��ȥ���´�½�Ĵ����ֲ��������㡣"
    + "\n" + "վ�ڼװ��ϣ�����Զ����Ϧ�������ƺ��ҵ��������µķ���";
                break;
        }


    }

    public void ReturnToBegin()
    {
        SceneManager.LoadScene("beginScene");
    }
}
