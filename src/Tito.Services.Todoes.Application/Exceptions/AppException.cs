using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public abstract string Code { get; }

        protected AppException(string value): base(value)
        {

        }
    }
}
