using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackInGame : MonoBehaviour
{
    public Button PauseBtn;

    public void Start()
    {
        PauseBtn.onClick.AddListener(BackToSelect);

    }

    private void BackToSelect()
    {
        AudioManager.Instance.PlayEffect("按钮2");
        GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");
        guideText.GetComponent<Text>().text = "点击确定返回游戏";
        guideText.SetActive(true);
    }
}
