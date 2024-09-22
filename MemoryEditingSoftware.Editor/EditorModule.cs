using MemoryEditingSoftware.Editor.ViewModels;
using MemoryEditingSoftware.Editor.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.Xml;

namespace MemoryEditingSoftware.Editor
{
    public class EditorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<EditorView, EditorViewModel>();
            containerRegistry.RegisterForNavigation<EditorView>();
        }
    }
}