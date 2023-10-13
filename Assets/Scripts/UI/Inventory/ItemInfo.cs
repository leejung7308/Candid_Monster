using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;
    [SerializeField]
    private TextMeshProUGUI txt_ItemValue;

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 0.5f, 0);
        go_Base.transform.position = _pos;

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        txt_ItemValue.text = (_item.itemValue/2).ToString();
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
