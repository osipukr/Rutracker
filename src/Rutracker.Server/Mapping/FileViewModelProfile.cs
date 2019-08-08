﻿using AutoMapper;
using Rutracker.Core.Entities.Torrents;
using Rutracker.Shared.ViewModels.Torrent;

namespace Rutracker.Server.Mapping
{
    public class FileViewModelProfile : Profile
    {
        public FileViewModelProfile() => CreateMap<File, FileItemViewModel>();
    }
}