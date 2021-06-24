using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using BSEvents = BS_Utils.Utilities.BSEvents;
using Reflection = BS_Utils.Utilities.ReflectionUtil;
using HMUI;
using BeatSaberMarkupLanguage;
using System.Reflection;
using SiraUtil.Zenject;

namespace ImageCoverExpander
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }


        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        /// 

        public Plugin(Zenjector zenjector, IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("ImageCoverExpander initialized.");
            zenjector.OnMenu<MenuInstaller>();
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

    }
}
