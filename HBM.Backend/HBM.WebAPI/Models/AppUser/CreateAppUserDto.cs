using AutoMapper;
using HBM.Application.AppUsers.Commands.CreateAppUser;
using HBM.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace HBM.WebAPI.Models.AppUser
{
    public class CreateAppUserDto : IMapWith<CreateAppUserCommand>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAppUserDto, CreateAppUserCommand>()
                .ForMember(appUserCommand => appUserCommand.Id,
                opt => opt.MapFrom(appUserDto => appUserDto.Id))
                .ForMember(appUserCommand => appUserCommand.UserName,
                opt => opt.MapFrom(appUserDto => appUserDto.UserName))
                .ForMember(appUserCommand => appUserCommand.Role,
                opt => opt.MapFrom(appUserDto => appUserDto.Role));
        }
    }
}