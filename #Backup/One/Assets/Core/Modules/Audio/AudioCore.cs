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

using UnityEngine.Audio;

using System.Runtime.Serialization;

namespace Game
{
    public abstract class AudioCoreBase : Core.DataModule<AudioData>
    {
        new public const string MenuPath = Core.Module.MenuPath + "Audio/";

        [SerializeField]
        protected AudioMixer mixer;
        public AudioMixer Mixer { get { return mixer; } }

        [SerializeField]
        protected AudioCoreVolume volume;
        public AudioCoreVolume Volume { get { return volume; } }

        public override void Configure()
        {
            base.Configure();

            volume.Configure();
        }

        public override void ResetData()
        {
            base.ResetData();

            data.Volumes = new List<AudioData.VolumeData>();
        }

        public override bool CheckData(AudioData data)
        {
            if (data.Volumes == null) return false;

            return base.CheckData(data);
        }

        public override void Init()
        {
            base.Init();

            volume.Init();
        }

        public AudioCoreBase()
        {
            fileName = nameof(AudioData) + ".sav";
        }
    }

    [CreateAssetMenu(menuName = MenuPath + "Asset")]
	public partial class AudioCore : AudioCoreBase
    {
        
    }

    [Serializable]
    [DataContract]
    public struct AudioData
    {
        [SerializeField]
        [DataMember]
        List<VolumeData> volumes;
        public List<VolumeData> Volumes { get { return volumes; } set { volumes = value; } }
        public float GetVolume(string name, float defaultValue)
        {
            for (int i = 0; i < volumes.Count; i++)
                if (volumes[i].Name == name)
                    return volumes[i].Value;

            return defaultValue;
        }
        public void SetVolume(string name, float value)
        {
            for (int i = 0; i < volumes.Count; i++)
            {
                if (volumes[i].Name == name)
                {
                    volumes[i].Value = value;
                    return;
                }
            }

            volumes.Add(new VolumeData(name, value));
        }

        [Serializable]
        [DataContract(Name = "Data")]
        public class VolumeData
        {
            [SerializeField]
            [DataMember]
            protected string name;
            public string Name { get { return name; } }

            [SerializeField]
            [DataMember]
            [Range(0f, 1f)]
            protected float value;
            public float Value
            {
                get
                {
                    return value;
                }
                set
                {
                    this.value = Mathf.Clamp01(value);
                }
            }

            public VolumeData(string name, float value)
            {
                this.name = name;
                this.value = value;
            }
        }
    }
}