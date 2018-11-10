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
        protected override void SetData(VideoData data)
        {
            base.SetData(data);

            SetQuality(data.Quality);
        }
        protected override void ApplyData(VideoData data)
        {
            base.ApplyData(data);

            ApplyQuality(data.Quality);
        }

        public virtual void SetQuality(int value)
        {
            data.Quality = value;

            ApplyQuality(data.Quality);

            SaveData();
        }
        public virtual void ApplyQuality(int value)
        {
            QualitySettings.SetQualityLevel(value, true);
        }

        public override bool CheckData(VideoData data)
        {
            data.Quality = Mathf.Clamp(data.Quality, 0, VideoData.MaxQuality);

            return base.CheckData(data);
        }

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

    [Serializable]
    [DataContract]
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