using MvvmNano.Forms;
using Xamarin.Forms;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Modules.Cities
{
    public class CityListPage : MvvmNanoContentPage<CityListViewModel>
    {
        private readonly ListView _listView;
        
        public CityListPage()
        {
            Title = "Cities";

            Content = _listView = new ListView();
            
            _listView.ItemSelected += OnItemSelected;
            
            _listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            _listView.ItemTemplate.SetBinding(TextCell.TextProperty, nameof(User.Name));
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            _listView.ItemsSource = ViewModel.Cities;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs arguments)
        {
            if (arguments.SelectedItem == null)
            {
                return;
            }
            
            const string cancel = "Cancel";
            
            var action = await DisplayActionSheet(
                "Do you want to remove that city from the list?", 
                cancel: cancel, 
                destruction: "Yes, remove it"
            );

            if (action != cancel)
            {
                ViewModel.RemoveCityCommand.Execute(arguments.SelectedItem);    
            }
            
            _listView.SelectedItem = null;
        }

        public override void Dispose()
        {
            base.Dispose();

            _listView.ItemSelected -= OnItemSelected;
        }
    }
}
