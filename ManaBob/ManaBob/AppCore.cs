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
    public class AppCore : 
            Xamarin.Forms.Application
    {
        Repository services   = new Repository();
        Repo<NavigationPage> pages = new Repo<NavigationPage>();
        //Repository pages = new Repository();

        Navigator navi;

        //INetService _net, ILocalService _local, IAuthService _auth
        public AppCore()
        {
            // Argument check
            //if(_net == null || _local == null || _auth == null)
            //{
            //    throw new ArgumentNullException("AppCore initialization failed");
            //}

            // Net/Local services
            // ---- ---- ---- ---- ----

            //services.Register<INetService>(_net);
            //services.Register<ILocalService>(_local);
            //services.Register<IAuthService>(_auth);


            // Pages / Navigation
            // ---- ---- ---- ---- ----

            navi = new Navigator(this);

            //var roomlist = new NavigationPage(new RoomList(navi, pages));
            //pages.Register<RoomList>(roomlist);
            var chatroom = new NavigationPage(new ChatRoom(navi, pages));
            pages.Register<RoomList>(chatroom);
            // Mandatory for Framework's initialization
            this.MainPage = chatroom;
        }

        protected override void OnStart()
        {
            this.MainPage.DisplayAlert("OnStart", "Starting!", "accept", "cancel");
        }

        protected override void OnSleep()
        {
            //this.MainPage.DisplayAlert("OnSleep", "sleeping", "accept", "cancel");
        }

        protected override void OnResume()
        {
            this.MainPage.DisplayAlert("OnResume", "resumed", "accept", "cancel");
        }
    }

}// namespace ManaBob
