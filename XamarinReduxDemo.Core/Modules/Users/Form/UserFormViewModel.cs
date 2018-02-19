using System;
using System.Linq;
using MvvmNano;
using XamarinReduxDemo.Core.Shared;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Modules.Users
{
    public class UserFormViewModel : ViewModelBase<UserId>
    {
        private UserId _userId;

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(); }
        }
        
        public string ImageName { get; private set; }
        
        private MvvmNanoCommand _saveCommand;
        public MvvmNanoCommand SaveCommand 
            => _saveCommand ?? 
               (_saveCommand = new MvvmNanoCommand(OnSave));

        // Hack, because there's no Close mechanism in MvvmNano yet
        public event EventHandler Done;
        
        public UserFormViewModel(IStore store) : base(store)
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
        
        private void OnSave()
        {
            var updatedUser = new User(
                _userId,
                Name
            );
            
            Store.Dispatch(StoreAction.NewUserUpdated(updatedUser));
            
            Done?.Invoke(this, EventArgs.Empty);
        }
    }
}
