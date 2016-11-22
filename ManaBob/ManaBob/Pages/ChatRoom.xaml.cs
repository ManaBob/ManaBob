using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ManaBob;
using ManaBob.ViewModel;

namespace ManaBob.Pages
{

    public static class ChatRoomTest
    {
        public static List<Message> GetMessages()
        {
            return new List<Message>
            {
                new Message(1111, 8888, Format.ToJson("JSON Text1")),
                new Message(2222, 8888, Format.ToJson("JSON Text2")),
                new Message(3333, 8888, Format.ToJson("JSON Text3")),
            };
        }

        public static Message GetMessage()
        {
            return new Message(1212, 8888, Format.ToJson("Text"));
        }
    }

	public partial class ChatRoom : ContentPage
	{
        Navigator navi;
        Repo<NavigationPage> pages;

        ChatRoomViewModel viewModel = new ChatRoomViewModel();


        public ChatRoom (Navigator _navi, Repo<NavigationPage> _pages)
		{
            navi = _navi;
            pages = _pages;

            // Load XAML
            InitializeComponent();

            this.BindingContext = viewModel;
            this.sendButton.Clicked += OnSendButtonClicked;
		}


        void MoreChats()
        {
            if(viewModel.Chats == null)
            {
                viewModel.Chats = new List<Chat>();
                return;
            }

            List<Chat> targetlist = new List<Chat>(viewModel.Chats);
            Chat chat = new Chat(ChatRoomTest.GetMessage());
            targetlist.Add(chat);
            //foreach (Message msg in ChatRoomTest.GetMessages())
            //{
            //    Chat chat = new ViewModel.Chat(msg);
            //    targetlist.Add(chat);
            //}
            // Update all
            viewModel.Chats = targetlist;

        }


        void OnSendButtonClicked(object _sender, EventArgs _ev)
        {
            this.MoreChats();
        }

    }
}
