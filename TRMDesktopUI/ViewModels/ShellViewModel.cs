using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private ILoggedInUserModel _user;

        public ShellViewModel(IEventAggregator events,
                              SalesViewModel salesVM,
                              ILoggedInUserModel user
                              )
        {
            _events = events;
            _salesVM = salesVM;
            _user = user;
            _events.Subscribe(this);

            // when deactivated the log in form will go away 
            // IoC is a thing Caliburn Micro brings in anyway, _container was too much
            ActivateItem(IoC.Get<LoginViewModel>());
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
            TryClose();
        }

        public void LogOut()
        {
            _user.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);

            //// if _loginVM was Dependency Injected to remember already filled in data
            //// more useful for a cart
            //_loginVM = _container.GetInstance<LoginViewModel>();
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
