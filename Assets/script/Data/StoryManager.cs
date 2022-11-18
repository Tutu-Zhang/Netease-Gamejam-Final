using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    private int levelCount;

    private GameObject dialogueText;
    private string dialogue;
    private Dictionary<string, string> DialogueData;
    private int MaxCount;
    private int CurCount = 1;

    private Image BackgroundImg;
    private string imgPath;

    public Button button;

    public void Start()
    {
        GameConfigManager.Instance.Init();
        levelCount = LevelManager.Instance.level;

        //改变背景图物体
        BackgroundImg = GameObject.FindGameObjectWithTag("DialogueBackground").GetComponent<Image>();
        imgPath = "UI-img/CG/" + levelCount.ToString() + "before";
        Texture2D texture = Resources.Load<Texture2D>(imgPath);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        BackgroundImg.sprite = sp;


        //获取旁白物体
        dialogueText = GameObject.FindGameObjectWithTag("DialogueText");        
        MaxCount = int.Parse(ExcelReader.Instance.GetDialogue(levelCount,0,"before"));//获取对白数目
        //Debug.Log("共有"+MaxCount+"句话");
        ChangeDialogue();

        button.onClick.AddListener(ChangeDialogue);
    }

    private void ChangeDialogue()
    {
        AudioManager.Instance.PlayEffect("按钮2");

        //所有剧情文案加载完毕
        if (CurCount > MaxCount)
        {
            button.gameObject.SetActive(false);
            Invoke("GoToGame", 1f);
        }

        dialogue = ExcelReader.Instance.GetDialogue(levelCount, CurCount, "before");
        dialogueText.GetComponent<Text>().text = dialogue;
        CurCount += 1;


    }

    private void GoToGame()
    {
        SceneManager.LoadScene("game1");
    }
}
