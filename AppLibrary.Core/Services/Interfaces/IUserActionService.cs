using AppLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Services.Interfaces
{
    public interface IUserActionService
    {
        int Save(UserActionModel userAction);
    }
}
