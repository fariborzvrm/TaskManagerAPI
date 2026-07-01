using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.Exceptions
{
    internal class NotExistingTaskTitleException : Exception
    {
        public NotExistingTaskTitleException(string message) : base(message) { }
    }
}
