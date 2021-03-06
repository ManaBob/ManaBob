﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ManaBob;
using ManaBob.ViewModel;

namespace ManaBob.Pages
{

    public static class RoomListTest
    {
        public static List<Room> GetRoomList()
        {
            return new List<Room>
            {
                new Room {
                    Name = "2인 6천",   ID = 1, Menu = Room.MenuCode.Unknown,
                    Budget = 6000, Size = 2,   Capacity = 2,
                    Status = Room.StatusCode.Full
                },
                new Room {
                    Name = "HellWorld", ID = 2, Menu = Room.MenuCode.Unknown,
                    Budget = 7000, Size = 2,   Capacity = 4,
                    Status = Room.StatusCode.Open
                },
                new Room {
                    Name = "라면도 한식인가", ID = 3, Menu = Room.MenuCode.Korean,
                    Budget = 4400, Size = 3,   Capacity = 3,
                    Status = Room.StatusCode.Closed
                },
            };
        }
    }



	public partial class RoomList : ContentPage
	{
        Navigator            navi      = AppCore.Services.Resolve<Navigator>();
        Repo<ContentPage>    pages     = AppCore.Services.Resolve<Repo<ContentPage>>();
        RoomListViewModel    viewModel = AppCore.Services.Resolve<RoomListViewModel>();

        public RoomList ()
		{
            // Binding Context
            viewModel.AllRooms  = RoomListTest.GetRoomList();
            this.BindingContext = viewModel;

            // ---- ---- ---- ---- ----

            // Load XAML objects
            InitializeComponent();

            // Picker : Menu/Capacities
            foreach(String menu in viewModel.Menus)
            {
                menuPick.Items.Add(menu);
            }
            foreach (String size in viewModel.Capacities)
            {
                sizePick.Items.Add(size);
            }

            // ---- ---- ---- ---- ----

            roomListView.ItemSelected   += OnRoomSelected;
        }


        protected void OnRoomSelected(object _sender, SelectedItemChangedEventArgs _ev)
        {
            var item = _ev.SelectedItem as Room;
            if (item == null){
                return;
            }
            // Disable recursive event loop
            roomListView.SelectedItem = null;

            if (item.Status == Room.StatusCode.Full)
            {
                DisplayAlert("It's full!", "The room is already full", "OK");
                return;
            }

            var next = pages.Resolve<ChatRoom>();
            navi.PushAsyncTo(next);
        }

        protected async void OnLogoutButtonClicked(object _sender, EventArgs _ev)
        {
            bool success = await viewModel.Logout(AppCore.CurrentUser);
            if(success == true)
            {
                AppCore.CurrentUser = null;
                navi.PopAsync();
            }
            else
            {
                DisplayAlert("Logout Failed", "OnLogoutButtonClicked", "OK");
            }
        }

        protected void OnCreateBtnClicked(object _sender, EventArgs _ev)
        {
            var next = pages.Resolve<CreateRoom>();
            navi.PushAsyncTo(next);
        }

    }
}
