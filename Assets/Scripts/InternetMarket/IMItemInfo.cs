using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IMItemInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private GameObject go_IMBase;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;
    [SerializeField]
    private TextMeshProUGUI txt_ItemValue;

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        
        go_Base.SetActive(true);
        /*
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 0.5f, 0);
        go_Base.transform.position = _pos;
        */

        _pos = new Vector3(go_IMBase.GetComponent<RectTransform>().rect.width*1.57f,
                            go_IMBase.GetComponent<RectTransform>().rect.height * 0.82f, 0);
        go_Base.transform.position = _pos;


        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        txt_ItemValue.text = _item.itemValue.ToString();
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }

    public string GetItem()
    {
        return txt_ItemName.text;
    }
}
