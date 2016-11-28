using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using ManaBob.Services;
using ManaBob.ViewModel;
using ManaBob.Pages;


namespace ManaBob
{
    /// <summary>
    ///     Application Core of the ManaBob
    /// </summary>
    public class AppCore : 
            Xamarin.Forms.Application
    {
        /// <summary>
        ///     Current User's information. If `null`, it is anonymous mode.
        /// </summary>
        static public User       CurrentUser    { get; set; }

        /// <summary>
        ///     Net/Auth/Local services
        /// </summary>
        static public Repository Services       { get; set; }

        /// <summary>
        ///     Page dictionary to minimize GC latency and easy code
        /// </summary>
        Repo<NavigationPage>    pages       = new Repo<NavigationPage>();

        /// <summary>
        ///     Navigation Handle for UI
        /// </summary>
        Navigator               navi;


        /// <summary>
        ///     Construction
        /// </summary>
        public AppCore(INetService _net)
        {
            if(_net == null)
            {
                throw new ArgumentNullException();
            }
            navi = new Navigator(this);

            // Service Mediator
            Services = new Repository();

            // Operation
            Services.Register<INetService>(_net);

            // Utility
            Services.Register<Repo<NavigationPage>>(pages);
            Services.Register<Navigator>(navi);

            // ViewModel
            Services.Register<RoomListViewModel>(new RoomListViewModel());


            var intro       = new NavigationPage(new Pages.Intro());
            var roomlist    = new NavigationPage(new Pages.RoomList());
            //var chatroom = new NavigationPage(new ChatRoom(navi, pages));

            pages.Register<Intro>(intro);
            pages.Register<RoomList>(roomlist);
            //pages.Register<RoomList>(chatroom);

            // Mandatory for Framework's initialization
            this.MainPage = intro;
        }

        protected override void OnStart()
        {
            //this.MainPage.DisplayAlert("OnStart", "Starting!", "accept", "cancel");
        }

        protected override void OnSleep()
        {
            //this.MainPage.DisplayAlert("OnSleep", "sleeping", "accept", "cancel");
        }

        protected override void OnResume()
        {
            //this.MainPage.DisplayAlert("OnResume", "resumed", "accept", "cancel");
        }
    }

}// namespace ManaBob
