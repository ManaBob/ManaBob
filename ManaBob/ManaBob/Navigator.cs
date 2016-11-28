using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ManaBob
{
    /// <summary>
    ///     Navigation Handler for Xamarin.Forms application
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Application"/>
    /// <seealso cref="Xamarin.Forms.Page"/>
    public class Navigator
    {
        Xamarin.Forms.Application app;
        Stack<Page> stack;
        /// <summary>
        ///     Select the app to control navigation
        /// </summary>
        /// <seealso cref="Xamarin.Forms.Application"/>
        public Navigator(Xamarin.Forms.Application _app)
        {
            if(_app == null)
            {
                throw new ArgumentNullException();
            }
            this.app = _app;
            this.stack = new Stack<Page>();
        }

        /// <summary>
        ///     Directly move to the given page
        /// </summary>
        /// <seealso cref="Xamarin.Forms.Page"/>
        public void GoAsyncTo(Page _next)
        {
            if (_next == null) { return; }

            app.MainPage = _next;

            if(stack.Count > 0)
            {
                stack.Pop();
            }
            stack.Push(app.MainPage);
        }

        /// <summary>
        ///     Use stack to move to the given page
        /// </summary>
        /// <seealso cref="Xamarin.Forms.Page"/>
        public void PushAsyncTo(Page _next)
        {
            if (_next == null) { return; }
            //await app.MainPage.Navigation.PushAsync(_next);

            app.MainPage = _next;
            stack.Push(_next);
        }

        /// <summary>
        ///     Recover the previous page in the stack
        /// </summary>
        public void PopAsync()
        {
            if(CanGoBack == false){ return; }
            //await app.MainPage.Navigation.PopAsync();
            stack.Pop();
            app.MainPage = stack.First();
        }

        /// <summary>
        ///     Check the stack depth
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return stack.Count > 1;
            }
        }

    }
}
