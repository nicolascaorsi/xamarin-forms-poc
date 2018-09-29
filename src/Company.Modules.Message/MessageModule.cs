using Prism.Ioc;
using Prism.Modularity;
using Company.Modules.Message.UI.Detail;
using Company.Modules.Message.UseCases;
using Company.Modules.Message.UI.Views;

namespace Company.Modules.Message
{
    public class MessageModule : IModule
    {
        public static string MainPageUri = $"{nameof(MessageModule)}.{nameof(ListPage)}";
        public static string ListPageUri = MainPageUri;
        public static string DetailPageUri = $"{nameof(MessageModule)}.{nameof(DetailPage)}";

        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ListPage>(MainPageUri);
            containerRegistry.RegisterForNavigation<DetailPage>(DetailPageUri);
            containerRegistry.Register<IDownloadOlderMessagesUC, DownloadOlderMessagesUC_Mock>();
            containerRegistry.Register<IDownloadNewMessagesUC, DownloadNewMessagesUC_Mock>();
            containerRegistry.Register<IGetRecentMessagesUC, GetRecentMessagesUC>();
            containerRegistry.Register<IGetMessageUC, GetMessageUC>();
        }
    }
}