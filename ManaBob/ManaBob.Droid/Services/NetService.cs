using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Net;
using System.Net.Sockets;

using ManaBob.Services;

namespace ManaBob.Droid.Services
{
    /// <summary>
    ///     `INetService` Implementation for Android Platform
    /// </summary>
    /// <seealso cref="ManaBob.Services.INetService"/>
    public class NetService : INetService
    {
        Socket sock = new Socket(AddressFamily.InterNetworkV6, 
                                 SocketType.Stream, 
                                 ProtocolType.Tcp);

        public NetService(IPEndPoint serverEp)
        {
            sock.Connect(serverEp);
        }

        /// <summary>
        ///     Send some bytes asynchronously to server.
        /// </summary>
        private Task SendBytes(Byte[] _buffer)
        {
            return Task.Factory.StartNew(() =>
            {
                int len = sock.Send(_buffer);
            });
        }

        /// <summary>
        ///     Send Message to Server
        /// </summary>
        /// <seealso cref="Message"/>
        Task INetService.Send(Message _message)
        {
            var msg_json = Format.ToJson(_message);
            Byte[] buffer = Encoding.UTF8.GetBytes(msg_json);

            return this.SendBytes(buffer);
        }

        /// <summary>
        ///     Send Request to Server. Then wait for the response...
        /// </summary>
        /// <seealso cref="ManaBob.Request"/>
        /// <seealso cref="ManaBob.Response"/>
        Task<Response> INetService.Request(Request _req)
        {
            var req_json = Format.ToJson(_req);
            Byte[] buffer = Encoding.UTF8.GetBytes(req_json);

            var task = this.SendBytes(buffer);

            return Task<Response>.Factory.StartNew(() =>
            {
                task.Wait();
                return new Response(_req, false, Format.ToJson(_req));
            });
        }


        /// <summary>
        ///     Check if the socket is connected
        /// </summary>
        bool INetService.IsConnected
        {
            get
            {
                return sock.Connected;
            }
        }

        /// <summary>
        ///     Dispose the inner resources
        /// </summary>
        void IDisposable.Dispose()
        {
            sock.Dispose();
        }

    }
}