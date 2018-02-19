using MvvmNano;
using XamarinReduxDemo.Core.Shared;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Modules.Users
{
    public class UserListViewModel : ViewModelBase
    {
        public SmartCollection<User> Users { get; }

        private MvvmNanoCommand<User> _openUserProfileCommand;
        public MvvmNanoCommand<User> OpenUserProfileCommand 
            => _openUserProfileCommand ?? 
               (_openUserProfileCommand = new MvvmNanoCommand<User>(OnOpenUserProfile));

        public UserListViewModel(IStore store) : base(store)
        {
            Users = new SmartCollection<User>();
            
            ConnectToStore(state => state.Users, users =>
            {
                Users.Reset(users);
            });
        }
        
        private void OnOpenUserProfile(User selectedUser)
        {
            NavigateTo<UserProfileViewModel, UserId>(selectedUser.Id);
        }
    }
}
