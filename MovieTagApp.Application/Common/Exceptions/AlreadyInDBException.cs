using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Common.Exceptions
{
    public class AlreadyInDBException: Exception
    {
        public AlreadyInDBException()
        {

        }
        public AlreadyInDBException(string Message) : base(Message)
        {

        }
    }
}
