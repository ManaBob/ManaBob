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
        Repo<ContentPage>    pages       = new Repo<ContentPage>();

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
            Services.Register<Repo<ContentPage>>(pages);
            Services.Register<Navigator>(navi);

            // ViewModel
            Services.Register<RoomListViewModel>(new RoomListViewModel());
            Services.Register<ChatRoomViewModel>(new ChatRoomViewModel());
            Services.Register<CreateRoomViewModel>(new CreateRoomViewModel());


            var intro       = new Pages.Intro();
            var roomlist    = new Pages.RoomList();
            var chatroom    = new Pages.ChatRoom();
            var createroom  = new Pages.CreateRoom();

            pages.Register<Intro>(intro);
            pages.Register<RoomList>(roomlist);
            pages.Register<ChatRoom>(chatroom);
            pages.Register<CreateRoom>(createroom);

            // Mandatory for Framework's initialization
            navi.GoAsyncTo(intro);
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
