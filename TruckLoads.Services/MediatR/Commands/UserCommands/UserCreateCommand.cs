using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLoads.Services.DTOs;

namespace TruckLoads.Services.MediatR.Commands.UserCommands
{
    public class UserCreateCommand : IRequest<long>
    {
        public UserDto UserDto { get; set; }
    }
}
