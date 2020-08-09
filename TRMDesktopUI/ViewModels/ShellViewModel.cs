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
        private SimpleContainer _container;

        public ShellViewModel(IEventAggregator events,
                              SalesViewModel salesVM,
                              SimpleContainer container)
        {
            _events = events;
            _salesVM = salesVM;
            _container = container;

            _events.Subscribe(this);

            // when deactivated the log in form will go away 
            ActivateItem(_container.GetInstance<LoginViewModel>());
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
