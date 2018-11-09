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
	public abstract class VideoCoreBase : Core.DataModule<VideoData>
	{
        public override void ResetData()
        {
            base.ResetData();

            data.Quality = VideoData.MaxQuality;
        }

        public VideoCoreBase()
        {
            fileName = nameof(VideoData) + ".sav";
        }
    }

    [CreateAssetMenu(menuName = MenuPath + "Video")]
    public partial class VideoCore : VideoCoreBase
    {

    }

    [DataContract]
    [Serializable]
    public partial struct VideoData
    {
        [SerializeField]
        [DataMember]
        int quality;
        public int Quality
        {
            get
            {
                return quality;
            }
            set
            {
                value = Mathf.Clamp(value, 0, MaxQuality);

                quality = value;
            }
        }
        public static int MaxQuality { get { return QualitySettings.names.Length - 1; } }
    }
}