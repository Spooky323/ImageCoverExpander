using System;
using Zenject;
using Reflection = BS_Utils.Utilities.ReflectionUtil;
using HMUI;
using UnityEngine;

namespace ImageCoverExpander
{
    public class ArtworkViewManager : IInitializable
    {
        private StandardLevelDetailViewController _standardLevelViewController;

        private static Vector3 modifiedScale = new Vector3(5.2f, 4.1f, 0f);
        private static Vector3 modifiedPositon = new Vector3(-35f, -56f, 0f);
        private static float modifiedSkew = 0;

        public ArtworkViewManager(StandardLevelDetailViewController standardLevelDetailViewController)
        {
            _standardLevelViewController = standardLevelDetailViewController;
        }

        public void Initialize()
        {
            var levelview = Reflection.GetPrivateField<StandardLevelDetailView>(_standardLevelViewController, "_standardLevelDetailView");
            var levelbar = Reflection.GetPrivateField<LevelBar>(levelview, "_levelBar");
            ArtworkModification(levelbar);
        }

        private void ArtworkModification(LevelBar instance)
        {
            if (!instance) { return; }
            Plugin.Log.Notice("Changing artwork for " + instance.name);
            try
            {
                var image = Reflection.GetPrivateField<ImageView>(instance, "_songArtworkImageView");
                image.GetComponent<RectTransform>().localScale = modifiedScale;
                image.GetComponent<RectTransform>().localPosition = modifiedPositon;
                image.color = new Color(1, 1, 1, 0.5f);
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
