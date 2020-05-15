using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = Policies.IsAdmin)]
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService, IMapper mapper) : base(mapper)
        {
            _fileService = fileService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<FileView>> Search(int torrentId)
        {
            var files = await _fileService.ListAsync(torrentId);

            return _mapper.Map<IEnumerable<FileView>>(files);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<FileView> Get(int id)
        {
            var file = await _fileService.FindAsync(id);

            return _mapper.Map<FileView>(file);
        }

        [HttpPost("upload/{torrentId}")]
        public async Task<FileView> Post(int torrentId, [FromForm] IFormFile file)
        {
            var createdFile = await _fileService.AddAsync(torrentId, file);

            return _mapper.Map<FileView>(createdFile);
        }

        [HttpPost("upload/{torrentId}/list")]
        public async IAsyncEnumerable<FileView> Post(int torrentId, [FromForm] IFormCollection formCollection)
        {
            foreach (var file in formCollection.Files)
            {
                var createdFile = await _fileService.AddAsync(torrentId, file);

                yield return _mapper.Map<FileView>(createdFile);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _fileService.Delete(id);
        }
    }
}