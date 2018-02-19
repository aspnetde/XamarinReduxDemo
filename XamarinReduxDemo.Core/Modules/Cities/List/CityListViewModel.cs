using MvvmNano;
using XamarinReduxDemo.Core.Shared;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Modules.Cities
{
    public class CityListViewModel : ViewModelBase
    {
        public SmartCollection<City> Cities { get; }

        private MvvmNanoCommand<City> _removeCityCommand;
        public MvvmNanoCommand<City> RemoveCityCommand 
            => _removeCityCommand ?? 
               (_removeCityCommand = new MvvmNanoCommand<City>(OnRemoveCity));

        public CityListViewModel(IStore store) : base(store)
        {
            Cities = new SmartCollection<City>();
            
            ConnectToStore(state => state.Cities, cities =>
            {
                Cities.Reset(cities);
            });
        }
        
        private void OnRemoveCity(City city)
        {
            Store.Dispatch(StoreAction.NewCityRemoved(city));
        }
    }
}
