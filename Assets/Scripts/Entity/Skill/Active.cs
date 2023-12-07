using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Entity.Skill
{
    public class ThrowAlcoholBottle : ActiveSkill
    {
        Player _player;
        GameObject _projectilePrefab;

        public ThrowAlcoholBottle(Player player)
        {
            _player = player;
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/AlcoholProjectile");
            if (_projectilePrefab == null)
                Debug.Log("ThrowAlcoholBottle > Cannot Load Prefab Object!");
        }

        public override void Activate()
        {
            GameObject pj = UObject.Instantiate(_projectilePrefab, _player.transform.position, _player.transform.rotation);
            pj.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Force);      // TODO: 플레이어가 바라보는 방향으로 던지기.
        }
    }
    /**
     * 스킬 > 액티브 > 회식 권유
     * 본인보다 직급이 낮은 몬스터들에게서 어그로를 해제한다.
     * 보스 몬스터에게는 적용하지 않는다 (자기보다 직급이 높아서)
     */
    public class LetsGoDinner : ActiveSkill
    {
        Player _player;
        BlindArea ba;
        public LetsGoDinner(Player player)
        {
            _player = player;
            ba = player.GetComponentInChildren<BlindArea>();
        }

        public override void Activate()
        {
            _player.StartCoroutine(ba.Toggle());
            ba.GetBlindedTargets().ForEach(radar =>
            {
                Debug.Log($"Active > LetsGoDinner > Detected 'Radar' object: {(radar == null ? "null" : radar.ToString())}");
                radar.Blind();
            });
        }
    }
    /**
     * 스킬 > 카페인 > 에스프레소 더블 샷
     * 카페인 스택을 10 추가한다.
     */
    public class EspressoDoubleShot : ActiveSkill
    {
        EntityStatus target;
        public EspressoDoubleShot(EntityStatus target)
        {
            this.target = target;
        }
        public override void Activate()
        {
            target.caffeine += 10;
        }
    }
    
    /**
     * 스킬 > 액티브 > 전자담배
     * 주변으로 담배 연기를 뿜어, 주위의 적들에게서 어그로를 해제하고, 담배 스택 10 증가
     */
    public class ElectronicSmoking : ActiveSkill
    {
        Player _player;
        BlindArea ba;
        public ElectronicSmoking(Player player)
        {
            _player = player;
            ba = player.GetComponent<BlindArea>();
        }

        public override void Activate()
        {
            _player.StartCoroutine(ba.Toggle());
            ba.GetBlindedTargets().ForEach(radar =>
            {
                radar.Blind();
                radar.monster.nicotine += 10;
            });
        }
    }

    public class OneByOneSmoking : ActiveSkill
    {
        Player _player;
        GameObject _projectilePrefab;

        public OneByOneSmoking(Player player)
        {
            _player = player;
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/MarkProjectile");
        }

        public override void Activate()
        {
            GameObject pj = UObject.Instantiate(_projectilePrefab, _player.transform.position, _player.transform.rotation);
            pj.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Force);      // TODO: 플레이어가 바라보는 방향으로 던지기.
        }
    }
}
