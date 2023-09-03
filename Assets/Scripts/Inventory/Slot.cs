using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f; // 더블클릭 감지 시간 임계값

    public Item.Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage; //item count background image

    private Player thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
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

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else //weapon
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
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
}