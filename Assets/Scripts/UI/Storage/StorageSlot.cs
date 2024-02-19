using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StorageSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f;

    public Item.Item item;
    public int itemCount;
    public Image itemImage;
    private ItemToolTip theItemToolTip;
    private Player thePlayer;
    private Storage theStorage;
    private Inventory theInventory;
    [SerializeField]
    private GameObject go_CountImage;
    [SerializeField]
    private TextMeshProUGUI text_Count;

    private Rect baseRect;

    void Start()
    {
        theItemToolTip = FindObjectOfType<ItemToolTip>();
        thePlayer = FindObjectOfType<Player>();
        theStorage = FindObjectOfType<Storage>();
        theInventory = FindObjectOfType<Inventory>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item.Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        itemImage.color = item.GetComponent<SpriteRenderer>().color;

        if (item.itemCategory != Item.ItemCategory.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else if (item.itemCategory == Item.ItemCategory.Equipment)
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        else
        {

        }
        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
        HideToolTip();
    }

    private IEnumerator ResetDoubleClickFlag()
    {
        yield return new WaitForSeconds(doubleClickTimeThreshold);
        isDoubleClick = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.clickCount == 2)
        {
            isDoubleClick = true;
            StartCoroutine(ResetDoubleClickFlag());
        }

        if (theStorage.GetInventoryOpen() == true)
        {
            if (isDoubleClick)
            {
                if (item != null)
                {
                    //인벤토리로 들어감
                    for(int i = 0; i < itemCount; i++)
                    {
                        theInventory.AcquireItem(item);
                    }
                    ClearSlot();
                }
            }
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theItemToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theItemToolTip.HideToolTip();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragStorageSlot.instance.theDragSlot = this;
            DragStorageSlot.instance.DragSetImage(itemImage);
            DragStorageSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        HideToolTip();

        if (item != null)
            DragStorageSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*if (DragStorageSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragStorageSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragStorageSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragStorageSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            Instantiate(DragStorageSlot.instance.theDragSlot.item,
                thePlayer.transform.position + thePlayer.transform.forward,
                Quaternion.identity);
            DragStorageSlot.instance.theDragSlot.ClearSlot();

        }*/

        DragStorageSlot.instance.SetColor(0);
        DragStorageSlot.instance.theDragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragStorageSlot.instance.theDragSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item.Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragStorageSlot.instance.theDragSlot.item, DragStorageSlot.instance.theDragSlot.itemCount);

        if (_tempItem != null)
            DragStorageSlot.instance.theDragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragStorageSlot.instance.theDragSlot.ClearSlot();
    }
}
