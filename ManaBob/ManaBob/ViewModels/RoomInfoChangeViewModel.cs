using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using ManaBob.Services;


namespace ManaBob.ViewModel
{
    public class RoomInfoChangeViewModel :
            Xamarin.Forms.BindableObject
    {

        public RoomInfoChangeViewModel()
        {

            Menus = new List<String>
            {
                "한식","중식","일식","양식","학식","분식","기타"
            };

            Capacities = new List<String>
            {
                "2명", "3명", "4명", "기타"
            };
        }


        /// <summary>
        ///     작성한 값을을 사용해 Room 정보를 수정
        /// </summary>
        public void Open()
        {
            throw new NotImplementedException();
        }


        public List<String> Menus { get; set; }
        public List<String> Capacities { get; set; }

    }

}// namespace ManaBob.ViewModel
