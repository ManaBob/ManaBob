using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using Xamarin.Forms;

using ManaBob.Services;

namespace ManaBob.ViewModel
{


    public class RoomListViewModel : 
            Xamarin.Forms.BindableObject
    {

        INetService netSvc = AppCore.Services.Resolve<INetService>();

        /// <summary>
        ///     Viewmodel Initialization
        /// </summary>
        public RoomListViewModel()
        {
            Menus = new List<String>
            {
                "Unknown","Korean"
            };

            Capacities = new List<String>
            {
                "1", "2", "3", "4"
            };

            AllRooms = new List<Room>();
            FilteredRooms = AllRooms;
        }


        /// <summary>
        ///     사용자가 지정한 방에 입장.
        /// </summary>
        public void Enter(Room _room)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Logout(User _currentUser)
        {
            Request reqOut = new Request(_currentUser,
                                         Request.Category.Logout,
                                         "Good Bye!");

            Response res = await netSvc.Send(reqOut);
            return res.Success;
        }


        public List<Room>   FilteredRooms { get; set; }
        public List<Room>   AllRooms      { get; set; }

        public List<String>     Menus       { get; set; }
        public List<String>     Capacities  { get; set; }

    }

}// namespace ManaBob.ViewModel
