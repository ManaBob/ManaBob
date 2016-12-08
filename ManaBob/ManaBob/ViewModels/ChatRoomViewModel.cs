using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using ManaBob.Services;

namespace ManaBob.ViewModel
{
    public class Chat
    {
        public Chat(Message _msg)
        {
            var bytes = _msg.Bytes;
            if (_msg.Type == Message.Format.Text)
            {
                this.Text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            }
            else
            {
                this.Text = "The message is Binary Format";
            }

            this.Sender = _msg.From.ToString();
        }

        public String Sender    { get; set; }
        public String Text      { get; set; }

    }


    public class ChatRoomViewModel : 
            Xamarin.Forms.BindableObject
    {
        INetService     netSvc      = AppCore.Services.Resolve<INetService>();

        List<Chat>      chatlist    = new List<Chat>();

        public ChatRoomViewModel()
        {

        }

        /// <summary>
        ///     서버로 메세지를 전송
        /// </summary>
        public Task<Message> Send(Message _msg)
        {
            return Task<Message>.Factory.StartNew(()=>
            {
                try
                {
                    netSvc.Send(_msg);
                    // Server will set the valid ID for the message.
                    // If not, the ID value is 0
                    _msg.ID = 239164;
                    return _msg;
                }
                catch(Exception _exc)
                {
                    return _msg;
                }
            });
            
        }

        /// <summary>
        ///     방으로 진입
        /// </summary>
        public async void Enter()
        {
            Request roomEnter = new Request(AppCore.CurrentUser,
                                            Request.Category.EnterRoom,
                                            "Enter");

            Response res = await netSvc.Send(roomEnter);

            if(res.Success == true)
            {

            }

        }

        /// <summary>
        ///     현재 방을 떠남
        /// </summary>
        public async void Leave()
        {
            Request roomLeave = new Request(AppCore.CurrentUser,
                                            Request.Category.LeaveRoom,
                                            "Leave");

            Response res = await netSvc.Send(roomLeave);

            if (res.Success == true)
            {

            }
        }

        /// <summary>
        ///     방의 내용을 갱신, 자동/수동 Sync 기능
        /// </summary>
        public void Update()
        {
            OnPropertyChanged();
        }


        ///// <summary>
        /////     Trigger UI update 
        ///// </summary>
        //public void NotifyChange()
        //{
        //    OnPropertyChanged();
        //}

        public List<Chat> Chats
        {
            get
            {
                return chatlist;
            }
            set
            {
                chatlist = value;
                OnPropertyChanged();
            }
        }

    }

}// namespace ManaBob.ViewModel
