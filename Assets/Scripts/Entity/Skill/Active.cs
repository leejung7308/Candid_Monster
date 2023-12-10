using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Entity.Skill
{
    public class ThrowAlcoholBottle : ActiveSkill
    {
        Player _player;
        GameObject _projectilePrefab;
        CaptureArea nearby;

        public ThrowAlcoholBottle(Player player)
        {
            _player = player;
            nearby = player.GetComponentInChildren<CaptureArea>();
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/AlcoholProjectile");
        }

        public override void Activate()
        {
            if(isCooldown)
                return;
            
            GameObject projectileObject = UObject.Instantiate(_projectilePrefab, _player.transform.position, _player.transform.rotation);
            Projectile pj = projectileObject.GetComponent<AoEProjectile>();
            _player.StartCoroutine(nearby.Toggle());
            
            try
            {
                Radar seenFirst = nearby.GetNearby();
                Debug.Log($"ActiveSkill > ThrowAlcoholBottle > Projectile Target: {seenFirst.gameObject.name}");
                pj.SetTarget(seenFirst.gameObject);
            }
            catch (InvalidOperationException e)
            {
                Debug.Log($"ActiveSkill > ThrowAlcoholBottle > Projectile has NO target.");
                pj.SetTarget(null);
            }
            
            StartCooldown();
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
        CaptureArea ba;
        public LetsGoDinner(Player player)
        {
            _player = player;
            ba = player.GetComponentInChildren<CaptureArea>();
        }

        public override void Activate()
        {
            if(isCooldown)
                return;
            
            _player.StartCoroutine(ba.Toggle());
            ba.GetCapturedTargets().ForEach(radar =>
            {
                Debug.Log($"Active > LetsGoDinner > Detected 'Radar' object: {(radar is null ? "null" : radar.ToString())}");
                radar.Blind();
            });
            
            StartCooldown();
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
            if(isCooldown)
                return;
            
            Debug.Log("ActiveSkill > EspressoDoubleShot: Activated!");
            target.caffeine += 10;
            
            StartCooldown();
        }
    }
    
    /**
     * 스킬 > 액티브 > 전자담배
     * 주변으로 담배 연기를 뿜어, 주위의 적들에게서 어그로를 해제하고, 담배 스택 10 증가
     */
    public class ElectronicSmoking : ActiveSkill
    {
        Player _player;
        CaptureArea ba;
        public ElectronicSmoking(Player player)
        {
            _player = player;
            ba = player.GetComponentInChildren<CaptureArea>();
        }

        public override void Activate()
        {
            if(isCooldown)
                return;
            
            _player.StartCoroutine(ba.Toggle());
            ba.GetCapturedTargets().ForEach(radar =>
            {
                radar.Blind();
                radar.monster.nicotine += 10;
            });
            
            StartCooldown();
        }
    }

    public class OneByOneSmoking : ActiveSkill
    {
        Player _player;
        GameObject _projectilePrefab;
        CaptureArea nearby;

        public OneByOneSmoking(Player player)
        {
            _player = player;
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/MarkProjectile");
            nearby = player.GetComponentInChildren<CaptureArea>();
        }

        public override void Activate()
        {
            if(isCooldown)
                return;
            
            GameObject projectileObject = UObject.Instantiate(_projectilePrefab, _player.transform.position, _player.transform.rotation);
            Projectile pj = projectileObject.GetComponent<SkillProjectile>();
            _player.StartCoroutine(nearby.Toggle());
            
            try
            {
                Radar seenFirst = nearby.GetNearby();
                Debug.Log($"ActiveSkill > OneByOneSmoking > Projectile Target: {seenFirst.gameObject.name}");
                pj.SetTarget(seenFirst.gameObject);
            }
            catch (InvalidOperationException e)
            {
                Debug.Log($"ActiveSkill > OneByOneSmoking > Projectile has NO target.");
                pj.SetTarget(null);
            }
            
            StartCooldown();
        }
    }
}
