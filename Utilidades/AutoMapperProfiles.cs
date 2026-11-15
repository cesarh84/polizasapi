using AutoMapper;
using polizas_api.Entidades;
using polizas_api.DTOs;

namespace polizas_api.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom(c => $"{c.Nombre} {c.Apellido}"));
        }
    }
}
