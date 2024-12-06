using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLoads.DataAccess.Repositories.IRepository;
using TruckLoads.Entity.Entities;
using TruckLoads.Services.MediatR.Commands.UserCommands;

namespace TruckLoads.Services.MediatR.Handlers.UserHandler
{
    public class UserCraeateHandler : IRequestHandler<UserCreateCommand, long>
    {
        private readonly IUserRepository _userRepository;
        public UserCraeateHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<long> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {

            User user = new User();
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            user.FirstName = request.UserDto.FirstName;
            user.LastName = request.UserDto.LastName;
            user.UserName = request.UserDto.UserName;
            user.UserId= request.UserDto.UserId;

            await _userRepository.AddUserAsync(user);
            return user.Id;
        }
    }
}
