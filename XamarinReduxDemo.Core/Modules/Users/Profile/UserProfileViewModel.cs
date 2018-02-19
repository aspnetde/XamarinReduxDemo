using System.Linq;
using MvvmNano;
using XamarinReduxDemo.Core.Shared;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Modules.Users
{
    public class UserProfileViewModel : ViewModelBase<UserId>
    {
        private UserId _userId;
        
        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(); }
        }
        
        public string ImageName { get; private set; }
        
        private MvvmNanoCommand _openFormCommand;
        public MvvmNanoCommand OpenFormCommand 
            => _openFormCommand ?? 
               (_openFormCommand = new MvvmNanoCommand(OnOpenForm));

        public UserProfileViewModel(IStore store) : base(store)
        {
        }
        
        public override void Initialize(UserId userId)
        {
            _userId = userId;
            
            ConnectToStore(state => state.Users.SingleOrDefault(u => Equals(u.Id, _userId)), user =>
            {
                if (user == null)
                {
                    return;
                }
                
                Name = user.Name;
                ImageName = user.Id.Item + ".jpg";
            });
        }
        
        private void OnOpenForm()
        {
            NavigateTo<UserFormViewModel, UserId>(_userId);
        }
    }
}
