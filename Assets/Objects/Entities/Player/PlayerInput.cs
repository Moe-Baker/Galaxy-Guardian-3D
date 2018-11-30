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
	public class PlayerInput : MonoBehaviour
	{
        [SerializeField]
        protected ImageHoldRelay holdRelay;
        public ImageHoldRelay HoldRelay { get { return holdRelay; } }

        public bool Shoot { get; protected set; } = false;

        protected Vector2 direction = Vector2.zero;
        public Vector2 Direction { get { return direction; } }

        public virtual void Process()
        {
            direction.x = (Input.mousePosition.x - Screen.width / 2f) / (Screen.width / 2f);
            direction.y = (Input.mousePosition.y - Screen.height / 2f) / (Screen.height / 2f);

            Shoot = holdRelay.IsClicked;
        }
	}
}