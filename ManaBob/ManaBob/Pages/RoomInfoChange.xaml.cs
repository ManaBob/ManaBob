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
    public partial class RoomInfoChange : ContentPage
    {
        Navigator navi = AppCore.Services.Resolve<Navigator>();
        Repo<ContentPage> pages = AppCore.Services.Resolve<Repo<ContentPage>>();

        INetService netSvc = AppCore.Services.Resolve<INetService>();
        RoomInfoChangeViewModel viewModel = AppCore.Services.Resolve<RoomInfoChangeViewModel>();

        public RoomInfoChange()
        {
            // Load XAML Component
            InitializeComponent();

            foreach (String menu in viewModel.Menus)
            {
                ChangeCategory.Items.Add(menu);
            }
            foreach (String size in viewModel.Capacities)
            {
                ChangePerson.Items.Add(size);
            }

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

            if (res.Success == false)
            {
                DisplayAlert("Creat Room failed", res.Reason, "OK");
                return;
            }

            var next = pages.Resolve<ChatRoom>();
            navi.GoAsyncTo(next);
        }

        protected void CancleButtonClicked(object _sender, EventArgs _ev)
        {
            //truncate all previous informations...

            // Go back
            navi.PopAsync();
        }

        protected void OnChangeInfoClicked(object _sender, EventArgs _ev)
        {
            // 방 정보 수정을 완료하면 정보가 바뀔수있도록 처리.
            navi.PopAsync();
        }
    }
}
