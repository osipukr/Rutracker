using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService, IMapper mapper) : base(mapper)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IEnumerable<FileView>> Search(int torrentId)
        {
            var files = await _fileService.ListAsync(torrentId);

            return _mapper.Map<IEnumerable<FileView>>(files);
        }

        [HttpGet("{id}")]
        public async Task<FileView> Get(int id)
        {
            var file = await _fileService.FindAsync(id);

            return _mapper.Map<FileView>(file);
        }
    }
}