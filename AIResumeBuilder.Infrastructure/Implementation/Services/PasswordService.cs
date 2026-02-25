using AIResumeBuilder.Application.Interfaces.Services;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string Password)
        => BCrypt.Net.BCrypt.HashPassword(Password);

        public bool VerifyPassword(string Password, string HashedPassword)
        =>BCrypt.Net.BCrypt.Verify(Password, HashedPassword);
        
    }
}
