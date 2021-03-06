﻿using System;
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
	public class GameMenu : MonoBehaviour
	{
		[SerializeField]
        protected Menu main;
        public Menu Main { get { return main; } }

        [SerializeField]
        protected Menu gameplay;
        public Menu Gameplay { get { return gameplay; } }

        [SerializeField]
        protected Menu end;
        public Menu End { get { return end; } }

        [SerializeField]
        protected PopupLabel popupLabel;
        public PopupLabel PopupLabel { get { return popupLabel; } }
    }
}