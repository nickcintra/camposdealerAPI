using AutoMapper;
using CamposDealer_React.Data;
using CamposDealer_React.Data.DTO_s;
using CamposDealer_React.Models;
using CamposDealer_React.Requests;
using Microsoft.EntityFrameworkCore;

namespace CamposDealer_React.Profiles
{
    public class VendaProfile : Profile
    {
        //private readonly APIContext _apicontext;

        //public VendaProfile(APIContext apicontext)
        //{
        //    _apicontext = apicontext;

        //    CreateMap<VendaRequest, Venda>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.DataVenda, opt => opt.MapFrom(src => DateTime.Now))
        //    .ForMember(dest => dest.Cliente, opt => opt.Ignore())
        //    .ForMember(dest => dest.VendaProdutos, opt => opt.MapFrom(src => src.Produtos));

        //    CreateMap<ProdutoVendaDTO, VendaProduto>()
        //        .ForMember(dest => dest.Id, opt => opt.Ignore())
        //        .ForMember(dest => dest.Venda, opt => opt.Ignore())
        //        .ForMember(dest => dest.Produto, opt => opt.Ignore());
        //}

        //public VendaProfile()
        //{

        //}
    }
}
