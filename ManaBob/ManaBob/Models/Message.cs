using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaBob
{

    /// <summary>
    ///     Serialization/Deserialization이 가능한 메세지
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     메세지 종류. 일부 확장자를 지원하는 것을 고려
        /// </summary>
        public enum Format
        {
            Binary = 0,
            Text
        }

        /// <summary>
        ///     Sender User's ID
        /// </summary>
        /// <seealso cref="User"/>
        public Int64    From    { get; set; }
        
        /// <summary>
        ///     Receiver Room's ID
        /// </summary>
        /// <seealso cref="Room"/>
        public Int64    To      { get; set; }

        /// <summary>
        ///     Message's unique ID in the room
        /// </summary>
        public UInt32 ID { get; set; }

        /// <seealso cref="Message.Format"/>
        public Format   Type    { get; set; }

        /// <summary>
        ///     Serialized Bytes
        /// </summary>
        public Byte[]   Bytes { get; set; }


        public Message() { }

        public Message(Int64 _from, Int64 _to, String _text)
        {
            From = _from;
            To = _to;
            Type = Format.Text;
            Bytes = Encoding.UTF8.GetBytes(_text);
        }

        public Message(Int64 _from, Int64 _to, Byte[] _bytes)
        {
            From = _from;
            To = _to;
            Type = Format.Binary;
            Bytes = _bytes;
        }
    }

}// namespace ManaBob

