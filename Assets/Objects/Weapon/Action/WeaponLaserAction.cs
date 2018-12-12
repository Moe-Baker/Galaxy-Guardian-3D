using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game
{
	public class WeaponLaserAction : WeaponAction
	{
		[SerializeField]
        protected Transform point;
        public Transform Point { get { return point; } }

        [SerializeField]
        protected LineRenderer line;
        public LineRenderer Line { get { return line; } }

        [SerializeField]
        protected float damagePerSecond = 200f;
        public float DamagePerSecond { get { return damagePerSecond; } }

        [SerializeField]
        protected float range = 100f;
        public float Range { get { return range; } }

        [SerializeField]
        protected LayerMask mask = Physics.DefaultRaycastLayers;
        public LayerMask Mask { get { return mask; } }

        protected virtual void Reset()
        {
            point = transform;

            line = Dependancy.Get<LineRenderer>(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();

            line.positionCount = 2;

            weapon.OnIdle += OnNoAction;
        }

        void OnNoAction()
        {
            line.enabled = false;
        }

        public override void Action()
        {
            base.Action();

            line.enabled = true;

            line.SetPosition(0, point.position);

            RaycastHit hit;

            if (Physics.Raycast(point.position, point.forward, out hit, range, mask))
            {
                var entity = hit.transform.GetComponent<Entity>();

                if(entity != null)
                {
                    weapon.Owner.DoDamage(entity, damagePerSecond * Time.deltaTime);
                }

                line.SetPosition(1, hit.point);
            }
            else
            {
                line.SetPosition(1, point.position + point.forward * range);
            }
        }
    }
}