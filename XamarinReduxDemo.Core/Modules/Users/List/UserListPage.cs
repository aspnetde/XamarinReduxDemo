using MvvmNano.Forms;
using Xamarin.Forms;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Modules.Users
{
    public class UserListPage : MvvmNanoContentPage<UserListViewModel>
    {
        private readonly ListView _listView;
        
        public UserListPage()
        {
            Title = "Users";

            Content = _listView = new ListView();
            
            _listView.ItemSelected += OnItemSelected;
            
            _listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            _listView.ItemTemplate.SetBinding(TextCell.TextProperty, nameof(User.Name));
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            _listView.ItemsSource = ViewModel.Users;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs arguments)
        {
            if (arguments.SelectedItem != null)
            {
                ViewModel.OpenUserProfileCommand.Execute(arguments.SelectedItem);    
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
