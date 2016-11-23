using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ManaBob;

namespace ManaBob.Pages
{

    public static class RoomListTest
    {
        public static List<Room> GetRoomList()
        {
            return new List<Room>
            {
                new Room {
                    Name = "Room !1",   ID = 1, Menu = Room.MenuCode.Unknown,
                    Budget = 6000, Size = 2,   Capacity = 2,
                    Status = Room.StatusCode.Full
                },
                new Room {
                    Name = "Room @2", ID = 2, Menu = Room.MenuCode.Unknown,
                    Budget = 7000, Size = 2,   Capacity = 4,
                    Status = Room.StatusCode.Open
                },
                new Room {
                    Name = "Room #3", ID = 3, Menu = Room.MenuCode.Kor,
                    Budget = 4400, Size = 3,   Capacity = 3,
                    Status = Room.StatusCode.Closed
                },
            };
        }
    }


    public class RoomDataTemplate
    {
        TextCell cell;
    }


	public partial class RoomList : ContentPage
	{
        Navigator navi;
        Repo<NavigationPage> pages;

        public RoomList (Navigator _navi, Repo<NavigationPage> _pages)
		{
            navi = _navi;
            pages = _pages;
			InitializeComponent ();

            var menuStrings = new List<String>
            {
                "한식",
                "양식",
                "일식",
                "중식",
                "학식"
            };

            foreach(var str in menuStrings)
            {
                menuPick.Items.Add(str);
            }

            //List<Room> rooms = RoomListTest.GetRoomList();
            roomListView.ItemTapped += (sender, args)=>
            {
                var item = args.Item as Room;
                if(item == null)
                {
                    return;
                }
               // this.appName.Text = item.Name;
                roomListView.SelectedItem = null;
            };


            roomListView.ItemTemplate = new DataTemplate(typeof(TextCell));
            roomListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");

            roomListView.ItemsSource = RoomListTest.GetRoomList();
        }

	}
}
