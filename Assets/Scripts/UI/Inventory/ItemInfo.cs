using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Item;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    public void ShowInfo(Item.Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width*1.5f, -go_Base.GetComponent<RectTransform>().rect.height*1.5f, 0);
        go_Base.transform.position = _pos;

        txt_ItemName.text = _item.itemName;
        switch (_item.itemCategory) 
        {
            case ItemCategory.EnchantItem:
                txt_ItemDesc.text = "종류 : \n인첸트 아이템\n" + "\n속성 :\n" + _item.itemType.ToString() + "\n\n추가 데미지 :\n" + _item.GetData().ToString();
                break;
            case ItemCategory.Equipment:
                txt_ItemDesc.text = "종류 : \n무기\n" + "\n속성 :\n" + _item.itemType.ToString() + "\n\n무기 데미지 :\n" + _item.GetData().ToString();
                break;
            case ItemCategory.Used:
                txt_ItemDesc.text = "종류 : \n소비 아이템\n" + "\n회복 수치 :\n" + _item.GetData().ToString();
                break;
            default:
                txt_ItemDesc.text = _item.itemDesc;
                break;
        }
    }

    public void HideInfo()
    {
        go_Base.SetActive(false);
    }
}
