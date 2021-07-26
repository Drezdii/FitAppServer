using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitAppServer.DTOs.Auth
{
    public class UserRegisterError
    {
        public string ErrorMessage { get; set; }
        public AuthErrorCode ErrorCode { get; set; }
    }
}
