using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ManaBob.Pages
{
    public partial class CreateRoom : ContentPage
    {
        Navigator navi;
        Repo<NavigationPage> pages;

        public CreateRoom(Navigator _navi, Repo<NavigationPage> _pages)
        {
            navi = _navi;
            pages = _pages;

            InitializeComponent();

            var strings = new List<String>
             {
                 "한식",
                 "중식",
                 "일식",
                 "양식",
                 "학식"
             };

            foreach (var str in strings)
            {
                category.Items.Add(str);
            }

            var per = new List<String>
             {
                 "2명",
                 "3명",
                 "4명",
                 "기타"
             };

            foreach (var str in per)
            {
                person.Items.Add(str);
            }
        }
    }  
}
