using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionDesc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    public Image collectionImage;

    public GameObject descHideImage;

    public void ShowDesc(Item.Item _item, Vector3 _pos)
    {
        descHideImage.SetActive(false);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        collectionImage.sprite = _item.GetComponent<SpriteRenderer>().sprite;
        collectionImage.color = _item.GetComponent<SpriteRenderer>().color;
    }

    public void UnknownDesc(Item.Item _item, Vector3 _pos)
    {
        descHideImage.SetActive(false);

        txt_ItemName.text = "";
        txt_ItemDesc.text = "";
        collectionImage.sprite = _item.GetComponent<SpriteRenderer>().sprite;
        collectionImage.color = Color.black;
    }
}
