using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Xamarin.Forms;

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
        List<Chat> chatlist;
        /// <summary>
        ///     서버로 메세지를 전송
        /// </summary>
        public void Send(Message _msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     방의 내용을 갱신, 자동/수동 Sync 기능
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     현재 방을 떠남
        /// </summary>
        public void Leave()
        {
            throw new NotImplementedException();
        }

        public List<Chat> Chats {
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
