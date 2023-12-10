using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Entity.Skill
{
    public class CaptureArea: MonoBehaviour
    {
        [SerializeField]
        float toggleDuration = 0.1f;
        Collider2D trigger;
        Dictionary<int, Radar> capturedMap;

        void Start()
        {
            trigger = GetComponent<Collider2D>();
            trigger.enabled = false;
            capturedMap = new Dictionary<int, Radar>();
        }

        public IEnumerator Toggle(float duration = 0.0f)
        {
            duration = duration > 0 ? duration : toggleDuration;
            trigger.enabled = true;
            yield return new WaitForSeconds(duration);
            trigger.enabled = false;
        }

        public void AddCapturedTarget(Radar target)
        {
            if(!capturedMap.ContainsKey(target.GetHashCode()))
            {
                capturedMap.Add(target.GetHashCode(), target);
            }
        }

        public List<Radar> GetCapturedTargets()
        {
            List<Radar> copied = new List<Radar>(capturedMap.Values);
            Debug.Log($"CaptureArea > Captured Targets: {copied.Count}");
            capturedMap.Clear();
            return copied;
        }

        public Radar GetNearby()
        {
            return capturedMap.Values.OrderBy(
                (radar) => Vector3.Distance(transform.position, radar.transform.position)
            ).First();
        }
    }
}
