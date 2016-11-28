using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ManaBob.Services;

namespace ManaBob.Pages
{
    public partial class Intro : ContentPage
    {
        Navigator navi              = AppCore.Services.Resolve<Navigator>();
        Repo<NavigationPage> pages  = AppCore.Services.Resolve<Repo<NavigationPage>>();

        INetService netSvc          = AppCore.Services.Resolve<INetService>();
        
        public Intro()
        {
            InitializeComponent();
        }


        protected async void OnLoginBtnClicked(object _sender, EventArgs _e)
        {
            if(EntryID.Text == null || EntryID.Text.Length == 0)
            {
                DisplayAlert("ID Error", "The field is empty!", "OK");
                return;
            }
            if (EntryPWD.Text == null || EntryPWD.Text.Length == 0)
            {
                DisplayAlert("Password Error", "The field is empty!", "OK");
                return;
            }


            User    tempUser    = new User { ID = 0, Name = EntryID.Text };
            Request reqLogin    = new Request(tempUser, 
                                              Request.Category.Login, 
                                              EntryPWD.Text);

            // Try to acquire the response.
            try
            {
                Response resLogin   = await netSvc.Send(reqLogin);
                LoginResponseHandler(resLogin);
            }
            catch(ArgumentNullException _exc)
            {
                DisplayAlert("Exception!", "Argument is null!", "OK");
                return;
            }
            catch (Exception _exc)
            {
                DisplayAlert("Exception!", _exc.Message, "OK");
                return;
            }

        }

        protected void LoginResponseHandler(Response _res)
        {
            if(_res.Success == false)
            {
                DisplayAlert("OnResponseCallback", "Login Failed", "OK");
            }

            // Server designate appropriate ID for the user.
            // tempUser's ID MUST BE CHANGED.
            if(_res.Sender.ID == 0)
            {
                throw new Exception("Invalid User ID");
            }

            // Sign in Complete. Remember the user
            AppCore.CurrentUser = _res.Sender;
            
            // Go to Room List
            var next = pages.Resolve<RoomList>();
            navi.PushAsyncTo(next);
        }

    }

}
