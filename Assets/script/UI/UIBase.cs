using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    //ע���¼�
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
        //��������ʵ��Close�Ĺ���
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
