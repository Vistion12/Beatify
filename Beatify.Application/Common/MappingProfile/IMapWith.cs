using AutoMapper;

namespace Beatify.Application.Common.MappingProfile;

public interface IMapWith<T>
{
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}
