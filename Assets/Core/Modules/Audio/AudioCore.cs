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

            data.Volumes = new Dictionary<string, float>();
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
        [DataMember]
        Dictionary<string, float> volumes;
        public Dictionary<string, float> Volumes { get { return volumes; } set { volumes = value; } }
        public float GetVolume(string name)
        {
            if (volumes.ContainsKey(name))
                return volumes[name];

            return 1f;
        }
        public void SetVolume(string name, float value)
        {
            if (volumes.ContainsKey(name))
                volumes[name] = value;
            else
                volumes.Add(name, value);
        }
    }
}