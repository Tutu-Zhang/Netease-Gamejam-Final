using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Excel;

public class ExcelReader : MonoBehaviour
{
    public static ExcelReader Instance = new ExcelReader();

    public void Awake()
    {

    }

    //����λ�û�ȡ��Ӧ����
    public string GetDialogue(int x,int y,string WhichToGet)
    {
        //Debug.Log("��ʼ��ȡexcel�ļ�");
        //��ȡ�����ļ�
        FileStream BeforeStream = File.Open(Application.streamingAssetsPath + "/DialogueBefore.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        FileStream AfterStream = File.Open(Application.streamingAssetsPath + "/DialogueAfter.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        
        //Debug.Log(BeforeStream);
        
        //���ļ����н�����
        IExcelDataReader BeforeExcelReader = ExcelReaderFactory.CreateOpenXmlReader(BeforeStream);//��ȡ 2007���Ժ�İ汾
        IExcelDataReader AfterExcelReader = ExcelReaderFactory.CreateOpenXmlReader(AfterStream);//��ȡ 2007���Ժ�İ汾

        //Debug.Log(BeforeExcelReader);
        //Debug.Log(BeforeExcelReader.AsDataSet());

        //��ȫ�����ݶ�ȡ����������result��
        DataSet BeforeDialogueResult = BeforeExcelReader.AsDataSet();
        DataSet AfterDialogueResult = AfterExcelReader.AsDataSet();
        //Debug.Log(BeforeDialogueResult.Tables[0].Rows[x][1].ToString());//��ӡcount
        x += 2;
        y += 1;

        string str = "";
        if (WhichToGet == "before")
        {                
            str = BeforeDialogueResult.Tables[0].Rows[x][y].ToString();
        }
        else if(WhichToGet == "after")
        {
            str = AfterDialogueResult.Tables[0].Rows[x][y].ToString();
        }
        else
        {
            str = "ExcelReader��ȡ�ļ�ʱ�д�����ȷ�Ĳ���";
        }
        return str;
    }
}
