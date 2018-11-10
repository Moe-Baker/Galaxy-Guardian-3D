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

using System.Runtime.Serialization;

namespace Game
{
	public abstract class VideoCoreBase : Core.Module
	{
        public int Quality
        {
            get
            {
                return PlayerPrefs.GetInt(QualityID, MaxQuality);
            }
            set
            {
                value = Mathf.Clamp(value, 0, MaxQuality);

                PlayerPrefs.SetInt(QualityID, value);
            }
        }
        public static string QualityID { get { return nameof(Quality); } }
        public static int MaxQuality { get { return QualitySettings.names.Length - 1; } }
    }

    [CreateAssetMenu(menuName = MenuPath + "Video")]
    public partial class VideoCore : VideoCoreBase
    {

    }
}