using Application.Dtos;
using CloudinaryDotNet.Actions;
using Domain.Comman;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
         public Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
       public Task<DeletionResult> DeletePhotoAsync(string publicId);
        public Task<Result<PhotoDto>> GetPhoto(string publicId);


        public Task<Result<PhotoDto>> AddPhotoToUser(Photo photo, string email);
    }
}
