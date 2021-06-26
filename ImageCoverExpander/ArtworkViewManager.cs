using System;
using Zenject;
using HMUI;
using UnityEngine;
using IPA.Utilities;

namespace ImageCoverExpander
{
    public class ArtworkViewManager : IInitializable
    {
        private StandardLevelDetailViewController _standardLevelViewController;

        private static readonly Vector3 modifiedScale = new Vector3(5.2f, 4.1f, 0f);
        private static readonly Vector3 modifiedPositon = new Vector3(-35f, -56f, 0f);
        private static readonly float modifiedSkew = 0;

        public ArtworkViewManager(StandardLevelDetailViewController standardLevelDetailViewController)
        {
            _standardLevelViewController = standardLevelDetailViewController;
        }

        public void Initialize()
        {
            var levelbar = _standardLevelViewController.transform.Find("LevelDetail").Find("LevelBarBig");
            ArtworkModification(levelbar);
        }

        private void ArtworkModification(Transform levelBarTranform)
        {
            if (!levelBarTranform) { return; }
            Plugin.Log.Notice("Changing artwork for " + levelBarTranform.name);
            try
            {
                var imageTransform = levelBarTranform.Find("SongArtwork");
                imageTransform.localScale = modifiedScale;
                imageTransform.localPosition = modifiedPositon;
                imageTransform.SetAsFirstSibling();
                var imageView = imageTransform.GetComponent<ImageView>();
                imageView.color = new Color(0.5f, 0.5f, 0.5f, 1);
                FieldAccessor<ImageView, float>.Set(ref imageView, "_skew", modifiedSkew);
            }
            catch (Exception e)
            {
                Plugin.Log.Error("Error changing artwork fields for " + levelBarTranform.name);
                Plugin.Log.Error(e);
            }
        }
    }
}
