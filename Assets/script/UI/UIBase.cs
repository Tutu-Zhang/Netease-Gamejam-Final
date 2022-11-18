using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    //注册事件
    public UIEventTrigger Register(string name)
    {
        Transform tf =  transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    public virtual void Close()
    {
        //用销毁来实现Close的功能
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
