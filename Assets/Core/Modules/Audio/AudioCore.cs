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

        #region Modules
        [SerializeField]
        protected AudioCoreVolume volume;
        public AudioCoreVolume Volume { get { return volume; } }

        public abstract class ModuleBase : Core.Module
        {
            new public const string MenuPath = AudioCore.MenuPath + "Modules/";

            public AudioCore Audio { get { return Core.Audio; } }
        }
        #endregion

        public override void Configure()
        {
            base.Configure();

            volume.Configure();
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

    [Serializable]
    [DataContract]
    public struct AudioData
    {

    }

    [CreateAssetMenu(menuName = MenuPath + "Asset")]
	public partial class AudioCore : AudioCoreBase
    {
        public partial class Module : ModuleBase
        {

        }
    }
}