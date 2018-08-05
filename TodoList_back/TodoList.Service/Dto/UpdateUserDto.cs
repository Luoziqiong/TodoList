using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service
{
    public class UpdateUserDto
    {
        public long Id;
        public string OldPassword;
        public string NewPassword;
    }
}
