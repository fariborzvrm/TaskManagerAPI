using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.Exceptions
{
    internal class DuplicateTaskTitleException : Exception
    {
        public DuplicateTaskTitleException(string message) : base(message) { }
    }
}
