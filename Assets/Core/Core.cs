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
using System.Xml;

namespace Game
{
    public abstract class CoreBase : ScriptableObject
    {
        public const string MenuPath = "Core" + "/";

        public static Core Asset { get; protected set; }

        #region Modules
        [SerializeField]
        protected AudioCore audio;
        public AudioCore Audio { get { return audio; } }

        [SerializeField]
        protected VideoCore video;
        public VideoCore Video { get { return video; } }

        public virtual void ForEachModule(Action<Core.Module> action)
        {
            action(audio);
            action(video);
        }

        public abstract class ModuleBase : ScriptableObject
        {
            public const string MenuPath = Core.MenuPath + "Modules/";

            public Core Core { get { return Core.Asset; } }

            public virtual void Configure()
            {

            }

            public virtual void Init()
            {

            }
        }

        public abstract class DataModuleBase<TData> : Core.Module
        {
            [SerializeField]
            protected string fileName;
            public string FileName { get { return fileName; } }
            public virtual string SavePath { get { return Path.Combine(Application.dataPath, fileName); } }

            [SerializeField]
            protected TData data;
            public TData Data { get { return data; } }

            public override void Configure()
            {
                base.Configure();

                ConfigureData();
            }

            protected virtual void ConfigureData()
            {
                if (File.Exists(SavePath))
                {
                    LoadData();
                }
                else
                {
                    ResetData();

                    SaveData();
                }
            }

            public virtual void SaveData()
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(TData));

                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

                using (XmlWriter writer = XmlWriter.Create(SavePath, settings))
                {
                    serializer.WriteObject(writer, data);
                }
            }

            public virtual void LoadData()
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(TData));

                using (FileStream file = new FileStream(SavePath, FileMode.Open))
                {
                    try
                    {
                        data = (TData)serializer.ReadObject(file);
                    }
                    catch (Exception)
                    {
                        Debug.LogError("Error Loading " + typeof(TData).Name);

                        file.Close();

                        ResetData();
                        SaveData();
                    }
                }
            }

            public virtual void ResetData()
            {
                data = Activator.CreateInstance<TData>();
            }
        }
        #endregion

        #region Tools
        public SceneAccessor SceneAccessor { get; protected set; }
        protected virtual void ConfigureSceneAccessor()
        {
            SceneAccessor = new GameObject("Scene Accessor").AddComponent<SceneAccessor>();
        }

        #endregion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnLoad()
        {
            Asset = Find();

            if(Asset == null)
            {
                Debug.LogWarning("No " + nameof(Core) + " asset found withing a Resources folder, Core operations will not work");
            }
            else
            {
                Asset.Configure();
            }
        }

        static Core Find()
        {
            var cores = Resources.LoadAll<Core>("");

            foreach (var core in cores)
            {
                if (core.name.ToLower().Contains("override"))
                    return core;
            }

            if (cores.Length > 0)
                return cores.First();
            else
                return null;
        }

        #region Configure
        protected virtual void Configure()
        {
            ConfigureSceneAccessor();

            SceneManager.sceneLoaded += OnSceneLoaded;

            ForEachModule(ConfigureModule);
        }

        protected virtual void ConfigureModule(Core.Module module)
        {
            module.Configure();
        }
        #endregion

        void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            Init();
        }

        #region Init
        protected virtual void Init()
        {
            ForEachModule(InitModule);
        }

        protected virtual void InitModule(Core.Module module)
        {
            module.Init();
        }
        #endregion
    }

    [CreateAssetMenu(menuName = MenuPath + "Asset")]
	public partial class Core : CoreBase
    {
        public partial class Module : ModuleBase
        {
            
        }

        public partial class DataModule<TData> : DataModuleBase<TData>
        {
            
        }
    }
}