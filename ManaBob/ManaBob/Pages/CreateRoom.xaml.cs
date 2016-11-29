using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ManaBob.Services;
using ManaBob.ViewModel;

namespace ManaBob.Pages
{
    public partial class CreateRoom : ContentPage
    {
        Navigator           navi        = AppCore.Services.Resolve<Navigator>();
        Repo<ContentPage>   pages       = AppCore.Services.Resolve<Repo<ContentPage>>();
        
        INetService         netSvc      = AppCore.Services.Resolve<INetService>();
        CreateRoomViewModel viewModel   = AppCore.Services.Resolve<CreateRoomViewModel>();

        public CreateRoom()
        {
            // Load XAML Component
            InitializeComponent();

            this.BindingContext = viewModel;
        }


        protected async void OnCreateClicked(object _sender, EventArgs _ev)
        {
            Room newRoom = new Room();
            newRoom.ID = 0;
            newRoom.Users.Add(AppCore.CurrentUser);


            Request createReq = new Request(AppCore.CurrentUser, 
                                            Request.Category.CreateRoom, 
                                            Format.ToJson(newRoom));

            Response res = await netSvc.Send(createReq);

            if(res.Success == false)
            {
                DisplayAlert("Creat Room failed", res.Reason, "OK");
                return;
            }

            var next = pages.Resolve<ChatRoom>();
            navi.GoAsyncTo(next);
        }

        protected void OnCancelClicked(object _sender, EventArgs _ev)
        {
            //truncate all previous informations...

            // Go back
            navi.PopAsync();
        }

    }  
}
