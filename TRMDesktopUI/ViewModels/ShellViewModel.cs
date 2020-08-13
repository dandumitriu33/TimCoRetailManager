using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private ILoggedInUserModel _user;
        private readonly IAPIHelper _apiHelper;

        public ShellViewModel(IEventAggregator events,
                              SalesViewModel salesVM,
                              ILoggedInUserModel user,
                              IAPIHelper apiHelper
                              )
        {
            _events = events;
            _salesVM = salesVM;
            _user = user;
            _apiHelper = apiHelper;
            _events.SubscribeOnPublishedThread(this);

            // when deactivated the log in form will go away 
            // IoC is a thing Caliburn Micro brings in anyway, _container was too much
            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public bool IsLoggedIn
        {
            get
            {
                bool output = false;

                if (String.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }

                return output;
            }
        }

        

        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }

        public async Task LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        //public void Handle(LogOnEvent message)
        //{
        //    ActivateItem(_salesVM);
        //    NotifyOfPropertyChange(() => IsLoggedIn);

        //    //// if _loginVM was Dependency Injected to remember already filled in data
        //    //// more useful for a cart
        //    //_loginVM = _container.GetInstance<LoginViewModel>();
        //}

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesVM, cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }



        #region DI test w/ Calculations
        //// testing Dependency Injection via simple class
        //private ICalculations _calculations;

        //public ShellViewModel(ICalculations calculations)
        //{
        //    _calculations = calculations;
        //}
        #endregion
    }
}
