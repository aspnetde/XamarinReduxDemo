using System;
using System.Collections.Generic;
using MvvmNano;
using XamarinReduxDemo.Store;

namespace XamarinReduxDemo.Core.Shared
{
    public abstract class ViewModelBase : MvvmNanoViewModel
    {
        private readonly IList<IDisposable> _disposables;

        protected readonly IStore Store;

        protected ViewModelBase(IStore store)
        {
            _disposables = new List<IDisposable>();

            Store = store;
        }
        
        protected void ConnectToStore<TSelection>(
            Func<AppState, TSelection> selector,
            Action<TSelection> callback
        )
        {
            var subscription = Store.Subscribe(
                SubscriptionType<TSelection>.NewSelectionSubscription(
                    selector,
                    callback
                )
            );
            _disposables.Add(subscription);

            var selection = selector(Store.CurrentState);
            callback(selection);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }
    }

    public abstract class ViewModelBase<TNavigationParameter> : MvvmNanoViewModel<TNavigationParameter>
    {
        private readonly IList<IDisposable> _disposables;

        protected readonly IStore Store;

        protected ViewModelBase(IStore store)
        {
            _disposables = new List<IDisposable>();

            Store = store;
        }
        
        protected void ConnectToStore<TSelection>(
            Func<AppState, TSelection> selector,
            Action<TSelection> callback
        )
        {
            var subscription = Store.Subscribe(
                SubscriptionType<TSelection>.NewSelectionSubscription(
                    selector,
                    callback
                )
            );
            _disposables.Add(subscription);

            var selection = selector(Store.CurrentState);
            callback(selection);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }
    }
}
