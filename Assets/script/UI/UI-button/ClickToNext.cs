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

        //很麻烦的写法，但是比再建一个txt简单多了
        PlayerPrefs.SetString("lv0guide1", "在赛博黑客的世界， [信息] 就是我们的武器。而在计算机中，信息便是由 [0] 和 [1] 组成。");
        PlayerPrefs.SetString("lv0guide2", "这便是你所看到的战斗界面。");
        PlayerPrefs.SetString("lv0guide3", "下面8位是 [信源区] ，每回合会有60%概率填充0，40%概率填充1。");
        PlayerPrefs.SetString("lv0guide4", "上面4个位置是 [信道区] ，将信源区的数字放入其中组合，可发动多种不同的技能");
        PlayerPrefs.SetString("lv0guide5", "组合的技巧在于，技能组合中1的数量越多，技能效果越强力。");
        PlayerPrefs.SetString("lv0guide6", "以1开头的组合更倾向于攻击，而0开头的组合则更倾向于防守。");
        PlayerPrefs.SetString("lv0guide7", "你将会面对不同的敌人。他们每个都有独门绝技，斗志和耐性更是技惊四座");
        PlayerPrefs.SetString("lv0guide8", "幸运的是，我会在战斗中辅助您，帮您显示不同组合的效果");
        PlayerPrefs.SetString("lv0guide9", "不幸的是...屏蔽效果即将失效。    敌人来袭！");
        PlayerPrefs.SetString("lv0guide0", "情况紧急，已为您暂时屏蔽敌人。接下来我将为您简单回忆战斗逻辑");
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
