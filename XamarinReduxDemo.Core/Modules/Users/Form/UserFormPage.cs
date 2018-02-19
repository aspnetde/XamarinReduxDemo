using System;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace XamarinReduxDemo.Core.Modules.Users
{
    public class UserFormPage : MvvmNanoContentPage<UserFormViewModel>
    {
        public UserFormPage()
        {
            Title = "Edit Profile";
        }
        
        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            ViewModel.Done += OnDone;
            
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Save",
                Command = ViewModel.SaveCommand
            });
       
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

            var nameEntry = new Entry
            {
                FontSize = 20,
                WidthRequest = 300,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Placeholder = "Name"
            };
            BindToViewModel(nameEntry, Entry.TextProperty, x => x.Name);

            layout.Children.Add(profileImage);
            layout.Children.Add(nameEntry);
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

        private void OnDone(object sender, EventArgs eventArgs)
        {
            Navigation.PopAsync(true);
        }

        public override void Dispose()
        {
            base.Dispose();

            ViewModel.Done -= OnDone;
        }
    }
}
