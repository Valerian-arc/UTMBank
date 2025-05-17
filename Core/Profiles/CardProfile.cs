using AutoMapper;
using Core.DTOs.CardDTOs;
using Core.Models.CardModels;
using Domain.Entites;

namespace Core.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile() 
        {
            CreateMap<Card, CardCreateDTO>().ReverseMap();
            CreateMap<Card, CardUpdateDTO>().ReverseMap();
            CreateMap<UpsertCardModel, Card>().ReverseMap();
            CreateMap<CardCreateDTO, UpsertCardModel>().ReverseMap();
            CreateMap<CardUpdateDTO, CardUpdateDTO>().ReverseMap();
            CreateMap<CardUpdateDTO,  UpsertCardModel>().ReverseMap();
        }
    }
}
