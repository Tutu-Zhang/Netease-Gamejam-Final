using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightRemake : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(GotoNew);
    }

    private void GotoNew()
    {
        AudioManager.Instance.PlayEffect("°´Å¥2");
        SceneManager.LoadScene("game1");
    }

}
