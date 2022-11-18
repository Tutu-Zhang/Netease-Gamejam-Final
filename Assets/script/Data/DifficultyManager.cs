using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    GameObject DiffText;
    public float DiffCount;

    private void Awake()
    {
        DiffCount = LevelManager.Instance.diffCount;

        //Debug.Log(DiffCount);

        DiffText = transform.Find("难度").gameObject;

        Slider difSlider = transform.Find("DifficultSlider").GetComponent<Slider>();

        difSlider.value = DiffCount;

        difSlider.onValueChanged.AddListener(ChangeDifficulity);
    }

    public void ChangeDifficulity(float value)
    {
        if (value >= 0.6f)
        {
            DiffText.GetComponent<Text>().text = "困难";

            LevelManager.Instance.AttackFix = 7;

            LevelManager.Instance.DefFix = 10;
        }
        else if (value >= 0.3f && value < 0.6f)
        {
            DiffText.GetComponent<Text>().text = "简单";

            LevelManager.Instance.AttackFix = 5;
            LevelManager.Instance.DefFix = 5;
        }
        else
        {
            DiffText.GetComponent<Text>().text = "宝宝";

            LevelManager.Instance.AttackFix = 0;
            LevelManager.Instance.DefFix = 0;
        }

        LevelManager.Instance.diffCount = value;
    }
}
