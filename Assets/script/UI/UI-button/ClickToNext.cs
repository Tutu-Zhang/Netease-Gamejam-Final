using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToNext : MonoBehaviour
{
    private GameObject clickToNext_button;
    private int count = 0;

    void Start()
    {
        clickToNext_button = GameObject.Find("/Canvas/GameWindow/clickToNext");
       


        if (LevelManager.Instance.level == 0)
        {
            clickToNext_button.SetActive(true);
            GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");
            guideText.SetActive(true);
        }

        //���鷳��д�������Ǳ��ٽ�һ��txt�򵥶���
        PlayerPrefs.SetString("lv0guide1", "�������ڿ͵����磬 [��Ϣ] �������ǵ����������ڼ�����У���Ϣ������ [0] �� [1] ��ɡ�");
        PlayerPrefs.SetString("lv0guide2", "���������������ս�����档");
        PlayerPrefs.SetString("lv0guide3", "����8λ�� [��Դ��] ��ÿ�غϻ���60%�������0��40%�������1��");
        PlayerPrefs.SetString("lv0guide4", "����4��λ���� [�ŵ���] ������Դ�������ַ���������ϣ��ɷ������ֲ�ͬ�ļ���");
        PlayerPrefs.SetString("lv0guide5", "��ϵļ������ڣ����������1������Խ�࣬����Ч��Խǿ����");
        PlayerPrefs.SetString("lv0guide6", "��1��ͷ����ϸ������ڹ�������0��ͷ�������������ڷ��ء�");
        PlayerPrefs.SetString("lv0guide7", "�㽫����Բ�ͬ�ĵ��ˡ�����ÿ�����ж��ž�������־�����Ը��Ǽ�������");
        PlayerPrefs.SetString("lv0guide8", "���˵��ǣ��һ���ս���и�������������ʾ��ͬ��ϵ�Ч��");
        PlayerPrefs.SetString("lv0guide9", "���ҵ���...����Ч������ʧЧ��    ������Ϯ��");
        PlayerPrefs.SetString("lv0guide0", "�����������Ϊ����ʱ���ε��ˡ��������ҽ�Ϊ���򵥻���ս���߼�");
        PlayerPrefs.Save();


        clickToNext_button.GetComponent<Button>().onClick.AddListener(NextGuide);
        
    }

    private void NextGuide()
    {
        if (count <= 9 && LevelManager.Instance.level == 0)
        {
            GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");
            guideText.GetComponent<Text>().text = PlayerPrefs.GetString("lv0guide" + count.ToString());

            count += 1;
        }
        else
        {
            GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");

            guideText.SetActive(false);
            clickToNext_button.SetActive(false);
        }
    } 
        
}
