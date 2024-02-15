using AutoMapper;
using ElevenNote.Data.Data;
using ElevenNote.Models.Categories;
using ElevenNote.Models.Notes;
using ElevenNote.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.Configurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<NoteEntity,NoteCreate>().ReverseMap();
            CreateMap<NoteEntity,NoteDetail>().ReverseMap();
            CreateMap<NoteEntity,NoteListItem>().ReverseMap();
            CreateMap<NoteEntity,NoteEdit>().ReverseMap();
            
            CreateMap<CategoryEntity,CategoryCreate>().ReverseMap();
            CreateMap<CategoryEntity,CategoryDetail>().ReverseMap();
            CreateMap<CategoryEntity,CategoryEdit>().ReverseMap();
            CreateMap<CategoryEntity,CategoryListItem>().ReverseMap();
            
            CreateMap<UserEntity,UserDetail>().ReverseMap();
        }
    }
}
