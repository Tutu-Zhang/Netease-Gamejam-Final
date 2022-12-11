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
        Debug.Log("执行文字改变");
        AudioManager.Instance.PlayBGM("结局");

        ChangText();
        BackToBegin.onClick.AddListener(ReturnToBegin);

    }

    public void ChangText()
    {

        Final = GameObject.Find("FinalText");
        switch (RoleManager.Instance.PlayerProfession)
        {

            case Professions.PALADIN:
                Final.GetComponent<Text>().text = "睁开眼，你漂浮在海面上。"+ "\n"+"漩涡早已无影无踪，之前发生的一切枉若一场梦。"+"\n"+ "根本没有净化世间的法宝。"+"\n"+ "你顿悟，有光的地方就有影，善与恶都是大千世界的一部分。"
                    +"\n"+ "人间并不是非黑即白，既不会变成地狱，也没法变成天堂。"+"\n"+ "与其在这杞人忧天，不如去用自己的学识去给予其他和过去的自己一样挣扎在泥潭中的人救赎。"+"\n"+ "一艘去往新大陆的船发现并救下了你。"
                    +"\n"+ "站在甲板上，望着远方的夕阳，你似乎找到了人生新的方向。" ;
                break;
            case Professions.MONK:
                Final.GetComponent<Text>().text = "睁开眼，你漂浮在海面上。" + "\n" + "漩涡早已无影无踪，之前发生的一切枉若一场梦。" + "\n" + "你战胜了挑战，却感到空虚无比。" + "\n" + "你思考，也许苦难并不值得赞美，磨炼意志是因为不得不如此才能生存。"
    + "\n" + "人们追求美好的生活，但苦尽之后也未必有甘来。" + "\n" + "与其禁锢自己的灵魂，自我折磨，不如更积极地去对待生活。" + "\n" + "一艘去往新大陆的船发现并救下了你。"
    + "\n" + "站在甲板上，望着远方的夕阳，你似乎找到了人生新的方向。";
                break;
            case Professions.SAMURAI:
                Final.GetComponent<Text>().text = "睁开眼，你漂浮在海面上。" + "\n" + "漩涡早已无影无踪，之前发生的一切枉若一场梦。" + "\n" + "根本没有无尽的宝藏和永恒的欢愉。" + "\n" + "你忽然想起了什么。"
    + "\n" + "想起那个在河边摸鱼的孩童，想起那个光着脚在田野间奔跑的身影。" + "\n" + "原来真正的最本质的快乐，一直被埋在自己心中。" + "\n" + "那些没有被欲望侮辱的，最纯粹的感情，才是珍贵的无尽宝藏。" + "\n"+"一艘去往新大陆的船发现并救下了你。"
    + "\n" + "站在甲板上，望着远方的夕阳，你似乎找到了人生新的方向。";
                break;
        }


    }

    public void ReturnToBegin()
    {
        SceneManager.LoadScene("beginScene");
    }
}
