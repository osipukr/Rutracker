﻿using AutoMapper;
using Rutracker.Core.Entities;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Mapping
{
    public class TorrentViewModelProfile : Profile
    {
        public TorrentViewModelProfile() => CreateMap<Torrent, TorrentItemViewModel>();
    }
}