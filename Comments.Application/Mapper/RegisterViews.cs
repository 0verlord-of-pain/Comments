using AutoMapper;
using Comments.Application.CQRS.Reviews.Queries.Views;
using Comments.Application.CQRS.Users.Queries.Views;
using Comments.Domain.Entities;

namespace Comments.Application.Mapper;

public sealed class RegisterViews : Profile
{
    public RegisterViews()
    {
        CreateMap<User, UserView>();
        CreateMap<Review, CommentView>()
            .ForMember(dest => dest.Email,
                dest => dest
                    .MapFrom(src => src.User.Email))
            .ForMember(dest => dest.NickName,
                dest => dest
                    .MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.ChildComment,
                dest => dest
                    .MapFrom(src => src.Reviews));
    }
}