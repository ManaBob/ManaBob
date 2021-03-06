﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaBob.Services
{
    public class FakeNet : INetService
    {
        bool INetService.IsConnected
        {
            get
            {
                return true;
            }
        }


        EventHandler<Notification> notiHandler = (s, e) => { };

        EventHandler<Notification> INetService.OnNotify
        {
            get{    return notiHandler;     }
            set{    notiHandler = value;    }
        }

        void IDisposable.Dispose()
        {
            return;
        }

        Task<Response> INetService.Send(Request _req)
        {
            // Argument check
            if(_req == null)
            {
                throw new ArgumentNullException();
            }

            // Fake Login
            if (_req.Type == Request.Category.Login)
            {
                return Task<Response>.Factory.StartNew(()=>
                {
                    var res = new Response(_req, true, Format.ToJson(_req));
                    // In real, server determines exact ID. This value is only for testing
                    res.Sender.ID = 298;
                    return res;
                });
            }

            // Fake Logout
            else if (_req.Type == Request.Category.Logout)
            {
                return Task<Response>.Factory.StartNew(() =>
                {
                    var res = new Response(_req, true, Format.ToJson(_req));
                    return res;
                });
            }

            // Fake Enter
            else if(_req.Type == Request.Category.EnterRoom)
            {
                return Task<Response>.Factory.StartNew(() =>
                {
                    var res = new Response(_req, true, Format.ToJson(_req));
                    return res;
                });

            }

            // Fake Leave
            else if (_req.Type == Request.Category.LeaveRoom)
            {
                return Task<Response>.Factory.StartNew(() =>
                {
                    var res = new Response(_req, true, Format.ToJson(_req));
                    return res;
                });

            }








            // The others are not implemented yet.
            else
            {
                throw new NotImplementedException();
            }

        }

        Task INetService.Send(Message _message)
        {
            return Task.Factory.StartNew(() => {
                return;
            });
        }
    }
}
