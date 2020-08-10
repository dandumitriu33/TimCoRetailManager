using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;

        public ShellViewModel(IEventAggregator events,
                              SalesViewModel salesVM
                              )
        {
            _events = events;
            _salesVM = salesVM;

            _events.Subscribe(this);

            // when deactivated the log in form will go away 
            // IoC is a thing Caliburn Micro brings in anyway, _container was too much
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);

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
