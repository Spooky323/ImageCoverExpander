using Zenject;

namespace ImageCoverExpander
{
    public class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ArtworkViewManager>().AsSingle();
        }
    }
}


