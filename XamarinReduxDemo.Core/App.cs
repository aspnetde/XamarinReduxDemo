using MvvmNano;
using MvvmNano.Forms;
using MvvmNano.TinyIoC;
using Xamarin.Forms;
using XamarinReduxDemo.Core.Modules.Cities;
using XamarinReduxDemo.Core.Modules.Users;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core
{
    public class App : MvvmNanoApplication
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpReduxStore();
            SetUpTabbedMainPage();
        }

        protected override IMvvmNanoIoCAdapter GetIoCAdapter() 
            => new MvvmNanoTinyIoCAdapter();

        private void SetUpReduxStore()
        {
            IStore store = CreateStore();
            
            MvvmNanoIoC.RegisterAsSingleton(store);
        }

        private static IStore CreateStore()
        {
            IStore store = new Store.Store(
                // Loads up the instance of the Store with the last known State.
                // In this example a new state is always being created, but that's
                // the place where you should load your current State from disk
                // or whatever you prefer for persisting data.
                InitialState.Create(),
                
                // Register all Reducers that should be applied to dispatched
                // actions.
                RootReducer.Create()
            );

            store.Subscribe(SubscriptionType<AppState>.NewStateSubscription(state =>
            {
                // The State was updated. That's the moment where you 
                // should persist the current state.
            }));

            return store;
        }

        private void SetUpTabbedMainPage()
        {
            var tabbedPage = new TabbedPage();

            tabbedPage.Children.Add(new MvvmNanoNavigationPage(GetPageFor<UserListViewModel>())
            {
                Title = "Users"
            });
            
            tabbedPage.Children.Add(new MvvmNanoNavigationPage(GetPageFor<CityListViewModel>())
            {
                Title = "Cities"
            });

            MainPage = tabbedPage;
        }
        
        private static Page GetPageFor<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            TViewModel viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize();
            
            return ResolvePage(viewModel);
        }
        
        private static Page ResolvePage<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            IView viewFor = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor<TViewModel>();
            viewFor.SetViewModel(viewModel);
            return viewFor as Page;
        }
    }
}
