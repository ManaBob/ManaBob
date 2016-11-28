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
        Navigator            navi      = AppCore.Services.Resolve<Navigator>();
        Repo<ContentPage>    pages     = AppCore.Services.Resolve<Repo<ContentPage>>();
        ChatRoomViewModel    viewModel = AppCore.Services.Resolve<ChatRoomViewModel>();

        public ChatRoom ()
		{
            InitializeComponent();            // Load XAML

            this.BindingContext = viewModel;
            this.sendButton.Clicked += OnSendButtonClicked;
		}

        // 메세지가 제대로 Display 되지 않음.
        protected async void OnSendButtonClicked(object _sender, EventArgs _ev)
        {
            Message msg = new Message(AppCore.CurrentUser.ID,
                                      AppCore.CurrentUser.ID,
                                      this.userInput.Text);

            msg = await viewModel.Send(msg);
            if(msg.ID == 0)
            {
                DisplayAlert("Send Failed", "OnSendButtonClicked", "OK");
                return;
            }

            Chat c = new ViewModel.Chat(msg);

            viewModel.Chats.Add(c);
            // Notify UI to update
            viewModel.NotifyChange();

            // Clear the message
            this.userInput.Text = "";
        }


        protected void OnBackButtonClicked(object _sender, EventArgs _ev)
        {
            navi.PopAsync();
        }
    }
}
