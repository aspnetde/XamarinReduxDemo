using MvvmNano.Forms;
using Xamarin.Forms;

namespace XamarinReduxDemo.Core.Modules.Users
{
    public class UserProfilePage : MvvmNanoContentPage<UserProfileViewModel>
    {
        public UserProfilePage()
        {
            Title = "Profile";
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
        
            var scrollView = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White
            };

            var layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Padding = 15,
                Spacing = 15
            };
            
            var profileImage = new Image
            {
                WidthRequest = 300,
                HeightRequest = 300,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Source = ImageSource.FromResource(ViewModel.ImageName)
            };

            var nameLabel = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };
            BindToViewModel(nameLabel, Label.TextProperty, x => x.Name);

            layout.Children.Add(profileImage);
            layout.Children.Add(nameLabel);
            scrollView.Content = layout;
            
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    scrollView
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Edit",
                Command = ViewModel.OpenFormCommand
            });
        }
    }
}
