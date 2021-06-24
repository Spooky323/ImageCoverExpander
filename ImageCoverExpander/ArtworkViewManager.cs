using System;
using System.Reflection;
using Zenject;
using Reflection = BS_Utils.Utilities.ReflectionUtil;
using HMUI;
using BeatSaberMarkupLanguage;
using UnityEngine;

namespace ImageCoverExpander
{
    class ArtworkViewManager : IInitializable
    {
        [Inject]
        private StandardLevelDetailViewController _standardLevelViewController;

        private static Vector3 modifiediedScale = new Vector3(5.2f, 4.1f, 0f);
        private static Vector3 modifiedPositon = new Vector3(-35f, -56f, 0f);
        private static float modifiedSkew = 0;



        public void Initialize()
        {
            var levelview = Reflection.GetPrivateField<StandardLevelDetailView>(_standardLevelViewController, "_standardLevelDetailView");
            var levelbar = Reflection.GetPrivateField<LevelBar>(levelview, "_levelBar");
            BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "ImageCoverExpander.Views.BackgroundMaskView.bsml"), levelbar.gameObject, this);
            var bsmlBackground = levelbar.transform.Find("BSMLBackground").gameObject;
            var artwork = Reflection.GetPrivateField<ImageView>(levelbar, "_songArtworkImageView");

            ArtworkModification(levelbar);
            levelbar.transform.Find("SongNameText").gameObject.transform.SetParent(bsmlBackground.transform);
            levelbar.transform.Find("AuthorNameText").gameObject.transform.SetParent(bsmlBackground.transform);
            bsmlBackground.transform.localPosition = new Vector3(0f, -6f, 0f);
            bsmlBackground.transform.Find("BSMLImage").gameObject.transform.localPosition = new Vector3(1.5f, -21.65f, 1.192093E-05f);
        }

        private void ArtworkModification(LevelBar instance)
        {
            if (!instance) { return; }
            Plugin.Log.Notice("Changing artwork for " + instance.name);
            try
            {
                var image = Reflection.GetPrivateField<ImageView>(instance, "_songArtworkImageView");
                image.GetComponent<RectTransform>().localScale = modifiediedScale;
                image.GetComponent<RectTransform>().localPosition = modifiedPositon;
                Reflection.SetPrivateField(image, "_skew", modifiedSkew);
            }
            catch (Exception e)
            {
                Plugin.Log.Error("Error changing artwork fields for " + instance.name);
                Plugin.Log.Error(e);
            }
        }
    }
}
