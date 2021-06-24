using Zenject;

namespace ImageCoverExpander
{
    class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ArtworkViewManager>().AsSingle();
        }
    }
}


