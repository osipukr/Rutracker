using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsUser)]
    public class FilesController : BaseApiController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService, IMapper mapper) : base(mapper)
        {
            _fileService = fileService;
        }

        [HttpGet("search"), AllowAnonymous]
        public async Task<IEnumerable<FileViewModel>> Search(int torrentId)
        {
            var files = await _fileService.ListAsync(torrentId);

            return _mapper.Map<IEnumerable<FileViewModel>>(files);
        }

        [HttpGet("{id}")]
        public async Task<FileViewModel> Find(int id)
        {
            var file = await _fileService.FindAsync(id, User.GetUserId());

            return _mapper.Map<FileViewModel>(file);
        }

        [HttpPost]
        public async Task<FileViewModel> Create(FileCreateViewModel model)
        {
            var file = _mapper.Map<File>(model);

            file = await _fileService.AddAsync(User.GetUserId(), file);

            return _mapper.Map<FileViewModel>(file);
        }

        [HttpPut("{id}")]
        public async Task<FileViewModel> Update(int id, FileUpdateViewModel model)
        {
            var file = _mapper.Map<File>(model);

            file = await _fileService.UpdateAsync(id, User.GetUserId(), file);

            return _mapper.Map<FileViewModel>(file);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _fileService.DeleteAsync(id, User.GetUserId());
        }
    }
}