using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using BSEvents = BS_Utils.Utilities.BSEvents;
using Reflection = BS_Utils.Utilities.ReflectionUtil;
using HMUI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BeatSaberMarkupLanguage;
using System.Reflection;

namespace ImageCoverExpander
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        private static Vector3 modifiediedScale = new Vector3(5.2f, 4.1f, 0f);
        private static Vector3 modifiedPositon = new Vector3(-35f, -56f, 0f);
        private static float modifiedSkew = 0;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("ImageCoverExpander initialized.");
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

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            new GameObject("ImageCoverExpanderController").AddComponent<ImageCoverExpanderController>();
            BSEvents.lateMenuSceneLoadedFresh += OnMenuSceneLoaded;
        }

        private void ArtworkModification(LevelBar instance)
        {
            if (!instance) { return; }
            Log.Notice("Changing artwork for "+instance.name +" ID - "+instance.GetInstanceID());
            try
            {
                var image = Reflection.GetPrivateField<ImageView>(instance, "_songArtworkImageView");
                image.GetComponent<RectTransform>().localScale = modifiediedScale;
                image.GetComponent<RectTransform>().localPosition = modifiedPositon;
                Reflection.SetPrivateField(image, "_skew", modifiedSkew);
            }
            catch(Exception e)
            {
                Log.Error("Error changing artwork fields for " + instance.name + " ID - " + instance.GetInstanceID());
            }
        }

        private void OnMenuSceneLoaded(ScenesTransitionSetupDataSO obj)
        {

            Log.Notice("Menu Loaded");
            try
            {
                var LevelDetailList = Resources.FindObjectsOfTypeAll<StandardLevelDetailView>();        
                foreach (var DetailInstance in LevelDetailList)
                {                    
                    var Levelbar = Reflection.GetPrivateField<LevelBar>(DetailInstance, "_levelBar");
                    BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "ImageCoverExpander.Views.BackgroundMaskView.bsml"), Levelbar.gameObject, this);
                    var bsmlBackground = Levelbar.transform.Find("BSMLBackground").gameObject;
                    var artwork = Reflection.GetPrivateField<ImageView>(Levelbar, "_songArtworkImageView");

                    ArtworkModification(Levelbar);
                    Levelbar.transform.Find("SongNameText").gameObject.transform.SetParent(bsmlBackground.transform);
                    Levelbar.transform.Find("AuthorNameText").gameObject.transform.SetParent(bsmlBackground.transform);
                    bsmlBackground.transform.localPosition = new Vector3(0f, -6f, 0f);
                    bsmlBackground.transform.Find("BSMLImage").gameObject.transform.localPosition = new Vector3(1.5f, -21.65f, 1.192093E-05f);
                }

            }
            catch (Exception e) 
            {
                Log.Error("Error changing fields : ");
                Console.WriteLine(e);
            }
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
