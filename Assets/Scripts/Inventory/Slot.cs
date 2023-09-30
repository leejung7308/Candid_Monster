using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f; // 더블클릭 감지 시간 임계값

    public Item.Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지

    [SerializeField]
    private TextMeshProUGUI text_Count;

    [SerializeField]
    private GameObject go_CountImage; //item count background image

    private Player thePlayer;
    private ItemInfo theItemInfo;

    private Rect baseRect;  // Inventory_Base 이미지의 Rect 정보 받아 옴.

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item.Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage; //acess item image

        if (item.itemType != Item.ItemType.Equipment && item.itemType != Item.ItemType.ETC)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else if(item.itemType == Item.ItemType.Equipment) //weapon
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        else
        {

        }

        SetColor(1);
    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
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
            if(item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    if (thePlayer != null)
                    {
                        thePlayer.EquipItem(item.itemName);
                        
                    }
                }
                else
                {
                    //Debug.Log(item.itemName + "을 사용했습니다.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theItemInfo.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theItemInfo.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            //Debug.Log(item);
            ShowToolTip(item, transform.position);
        }
    }

    // 마우스 커서가 슬롯에서 나올 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }

    //마우스 드래그가 시작됐을 때 발생하는 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    //마우스 드래그 중일 때 발생하는 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        HideToolTip();

        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    // 마우스 드래그가 끝났을 때 발생하는 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            Instantiate(DragSlot.instance.dragSlot.item,
                thePlayer.transform.position + thePlayer.transform.forward,
                Quaternion.identity);
            DragSlot.instance.dragSlot.ClearSlot();

        }

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // 해당 슬롯에 무언가가 마우스 드롭 됐을 때 발생하는 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item.Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}