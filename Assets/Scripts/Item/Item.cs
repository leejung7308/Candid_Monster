using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    /**
     * 아이템이 가지는 3가지 속성의 수치를 나타냅니다.
     * 몬스터 공격시 상성에 따른 피해 계산, 스킬로 인한 일부 수치의 변화 등을 용이하게 만들기 위해,
     * 속성 수치를 표현하는 별도의 컨테이너 클래스를 구현합니다.
     * 속성 수치를 참조해야하는 모든 종류의 함수에서, 아이템의 참조를 원치 않을 경우 DamageHolder 객체만 넘겨받아 처리할 수 있습니다.
     *
     * 예를 들어, 카페인 수치를 100% 증가시키는 스킬을 사용 후 카페인 수치를 요구하는 몬스터를 공격하는 과정은
     * 아래와 같은 순서로 추상화될 수 있습니다.
     *
     * Player().Attack(Monster m, Item i) => { newStat = Player().skill.apply(i.Stat) } => m.damage(newStat)
     * 1. 플레이어 객체가 몬스터 객체 m을 손에 들고 있는 아이템 i로 공격합니다.
     * 2. Attack 메소드 내부에서, 현재 플레이어에게 적용중인 스킬들을 하나씩 가져와 순차적으로 스킬을 반영합니다.
     *      각 스킬은 apply 혹은 기타 메소드를 통해 기존 스탯을 인자로 받아 스킬로 인해 수치가 조정된 새 스탯을 반환합니다.
     * 3. 모든 스킬을 반영해 계산된 최종 스탯 객체를 m의 damage 메소드로 전달합니다.
     * 4. 몬스터 객체의 damage 메소드에서 스텟에 따른 피해를 계산합니다.
     */
    public class DamageHolder
    {
        public readonly Player Player;
        public readonly EntityStatus Monster;
        public readonly float Caffeine;
        public readonly float Alcohol;
        public readonly float Nicotine;
        public readonly float Damage;

        public DamageHolder(float caffeine = 1.0f, float alcohol = 1.0f, float nicotine = 1.0f, float damage = 1.0f)
        {
            Caffeine = caffeine;
            Alcohol = alcohol;
            Nicotine = nicotine;
            Damage = damage;
        }
        
        public static DamageHolder operator +(DamageHolder a)
            => a;
        public static DamageHolder operator -(DamageHolder a)
            => new (-a.Caffeine, -a.Alcohol, -a.Nicotine, -a.Damage);
        
        public static DamageHolder operator +(DamageHolder a, DamageHolder b)
        => new (
            a.Caffeine + b.Caffeine,
            a.Alcohol + b.Alcohol,
            a.Nicotine + b.Nicotine,
            a.Damage + b.Damage
        );
        
        public static DamageHolder operator -(DamageHolder a, DamageHolder b)
            => a + (-b);
        
        public static DamageHolder operator *(DamageHolder a, float ratio) 
            => new (
                a.Caffeine * ratio,
                a.Alcohol * ratio,
                a.Nicotine * ratio,
                a.Damage * ratio
            );
        
        public static DamageHolder operator *(DamageHolder a, DamageHolder b)
            => new (
                a.Caffeine * b.Caffeine,
                a.Alcohol * b.Alcohol,
                a.Nicotine * b.Nicotine,
                a.Damage * b.Damage
            );
    }

    public enum ItemCategory  // 아이템 유형
    {
        None,
        Equipment, //weapon
        Used, //consumable
        Ingredient,
        ETC,
        EnchantItem,
    }
    public enum ItemType
    {
        None,
        alcohol,
        caffeine,
        nicotine,
    }
    /**
     * 아이템의 기본 클래스입니다. 인터페이스는 객체 멤버를 가질 수 없어, 클래스로 구현되었습니다.
     */
    public abstract class Item: MonoBehaviour
    {
        public string itemName;
        public ItemCategory itemCategory;
        public ItemType itemType;
        public Sprite itemImage;
        [TextArea]
        public string itemDesc;
        public int itemValue;
        public int itemAcquire = 0;
        public bool isPickedUp = false;


        public abstract void Use();
        public abstract int GetData();
        public abstract void AddData(int data);
        public abstract bool IsEnchanted();
        public abstract void Enchant();
        public virtual DamageHolder GetDamageHolder()
            => new DamageHolder(
                0.0f
            );
    }
}
