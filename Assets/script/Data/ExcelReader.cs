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

    //根据位置获取对应内容
    public string GetDialogue(int x,int y,string WhichToGet)
    {
        //Debug.Log("开始读取excel文件");
        //读取剧情文件
        FileStream BeforeStream = File.Open(Application.streamingAssetsPath + "/DialogueBefore.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        FileStream AfterStream = File.Open(Application.streamingAssetsPath + "/DialogueAfter.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        
        //Debug.Log(BeforeStream);
        
        //对文件进行解析？
        IExcelDataReader BeforeExcelReader = ExcelReaderFactory.CreateOpenXmlReader(BeforeStream);//读取 2007及以后的版本
        IExcelDataReader AfterExcelReader = ExcelReaderFactory.CreateOpenXmlReader(AfterStream);//读取 2007及以后的版本

        //Debug.Log(BeforeExcelReader);
        //Debug.Log(BeforeExcelReader.AsDataSet());

        //将全部数据读取出来，存在result里
        DataSet BeforeDialogueResult = BeforeExcelReader.AsDataSet();
        DataSet AfterDialogueResult = AfterExcelReader.AsDataSet();
        //Debug.Log(BeforeDialogueResult.Tables[0].Rows[x][1].ToString());//打印count
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
            str = "ExcelReader读取文件时有传入正确的参数";
        }
        return str;
    }
}
