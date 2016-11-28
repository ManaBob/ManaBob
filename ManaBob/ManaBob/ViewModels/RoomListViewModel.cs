using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using Xamarin.Forms;

namespace ManaBob.ViewModel
{

    public class RoomListViewModel : 
            Xamarin.Forms.BindableObject
    {

        /// <summary>
        ///     현재 보유한 Room 들을 특정 기준으로 필터링
        /// </summary>
        public static List<Room> Filter(List<Room> _old)
        {
            return _old;            
        }

        /// <summary>
        ///     주어진 Room 들을 특정 기준으로 재정렬
        /// </summary>
        public static List<Room> Sort(List<Room> _old)
        {
            return _old;
        }

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

        public List<Room> FilteredRooms { get; set; }
        public List<Room> AllRooms      { get; set; }

        public List<String>     Menus       { get; set; }
        public List<String>     Capacities  { get; set; }

    }

}// namespace ManaBob.ViewModel
