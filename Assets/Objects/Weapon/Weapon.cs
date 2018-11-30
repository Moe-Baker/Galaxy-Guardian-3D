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
	public class Weapon : MonoBehaviour
	{
        public Entity Owner { get; protected set; }

        public List<WeaponConstraint> Constraints { get; protected set; }
        public virtual bool CanUse
        {
            get
            {
                for (int i = 0; i < Constraints.Count; i++)
                    if (!Constraints[i].Active)
                        return false;

                return true;
            }
        }

        public virtual void Init(Entity owner)
        {
            this.Owner = owner;

            Constraints = Dependancy.GetAll<WeaponConstraint>(gameObject);
        }

        public delegate void ProcessDelegate(bool input);
        public event ProcessDelegate OnProcess;
        public virtual void Process(bool input)
        {
            if (OnProcess != null) OnProcess(input);

            if (input && CanUse)
                Action();
        }

        public event Action OnAction;
        public virtual void Action()
        {
            if (!CanUse) return;

            if (OnAction != null) OnAction();
        }
	}
}