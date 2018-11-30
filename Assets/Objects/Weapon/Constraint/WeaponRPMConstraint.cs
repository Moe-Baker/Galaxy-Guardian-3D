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
    public class WeaponRPMConstraint : WeaponConstraint
    {
        [SerializeField]
        protected int value = 800;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                value = this.value;

                if (value < 0)
                    value = 0;
            }
        }

        public float Delay
        {
            get
            {
                return 60f / value;
            }
        }

        float time = 0f;

        public override bool Active
        {
            get
            {
                return Mathf.Approximately(time, 0f);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            weapon.OnProcess += OnProcess;
            weapon.OnAction += OnAction;
        }

        protected virtual void OnProcess(bool input)
        {
            time = Mathf.MoveTowards(time, 0f, Time.deltaTime);
        }

        void OnAction()
        {
            time = Delay;
        }
    }
}