using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Skill
{
    public class BlindArea: MonoBehaviour
    {
        [SerializeField]
        float toggleDuration = 0.1f;
        Collider2D trigger;
        List<Radar> blindTargets;

        void Start()
        {
            trigger = GetComponent<Collider2D>();
            trigger.enabled = false;
            blindTargets = new List<Radar>();
        }

        public IEnumerator Toggle()
        {
            trigger.enabled = true;
            yield return new WaitForSeconds(toggleDuration);
            trigger.enabled = false;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Monster") || other.CompareTag("ConfusedMonster"))
            {
                Radar blinded = other.GetComponentInChildren<Radar>();
                if (blinded != null)
                    blindTargets.Add(blinded);
            }
        }

        public List<Radar> GetBlindedTargets()
        {
            List<Radar> copied = new List<Radar>(blindTargets);
            Debug.Log($"BlindArea > Blinded Targets: {copied.Count}");
            blindTargets.Clear();
            return copied;
        }
    }
}
