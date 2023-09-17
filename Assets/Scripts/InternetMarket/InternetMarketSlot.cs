using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarketSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f;

    public Item.Item item; //등록할 아이템
    public Image itemImage;  // 아이템의 이미지

    private IMItemInfo theIMItemInfo;
    [SerializeField]
    private Inventory theInventory;

    private Rect baseRect;

    // Start is called before the first frame update
    void Start()
    {
        AddItem();
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

    // 상점에 새로운 아이템 슬롯 추가
    public void AddItem()
    {
        // itemImage의 sprite를 item의 이미지로 업데이트합니다.
        if (item != null)
        {
            itemImage.sprite = item.itemImage; // itemImage를 item의 이미지로 설정
        }
        else
        {
            // item이 null이라면 이미지를 비워둘 수 있습니다.
            itemImage.sprite = null;
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theIMItemInfo.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theIMItemInfo.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            ShowToolTip(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }

    private IEnumerator ResetDoubleClickFlag()
    {
        yield return new WaitForSeconds(doubleClickTimeThreshold);
        isDoubleClick = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.clickCount == 2) // 더블클릭 감지
        {
            isDoubleClick = true;
            StartCoroutine(ResetDoubleClickFlag());
        }

        if (isDoubleClick)
        {
            if (item != null)
            {
                //Debug.Log(item.itemName + "을 구매했습니다.");
                theInventory.SetCoinText(item.itemValue);
            }
        }
    }
}
