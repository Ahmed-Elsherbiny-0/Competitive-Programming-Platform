using Application.Comman.Errors;
using Application.Dtos;
using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using Infrastructure.Options;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IGenericRepository<Photo> photoRepo;
        private readonly IGenericRepository<ApplicationUser> userRepo;

        public PhotoService(IOptions<CloudinarySettings> options, IGenericRepository<Photo> _photoRepo, IGenericRepository<ApplicationUser> _userRepo)
        {
            var acc = new Account(options.Value.CloudName, options.Value.ApiKey, options.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
            photoRepo = _photoRepo;
            userRepo = _userRepo;
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var result = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParms = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Gravity("face").Crop("fill"),
                    Folder = "mostaqel-platform"

                };
                result = await _cloudinary.UploadAsync(uploadParms);
            }
            return result;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var photo = await photoRepo.GetEntityWithspec(new PhotoSpacification(publicId));
            if (photo != null)
            {
                photoRepo.DeleteItem(photo);
                await photoRepo.SaveChangesAsync();
            }
            return await _cloudinary.DestroyAsync(deleteParams);

        }
        public async Task<Result<PhotoDto>> GetPhoto(string publicId)
        {
            var photo = await photoRepo.GetEntityWithspec(new PhotoSpacification(publicId));
            if (photo == null) return Result.Failure<PhotoDto>(PhotoErrors.PhotoNotFounded);
            return Result.Success(photo.Adapt<PhotoDto>());
        }


        public async Task<Result<PhotoDto>> AddPhotoToUser(Photo photo, string email)
        {


            var user = await userRepo.GetEntityWithspec(new UserSpecification(email));
            if (user == null)
                return Result.Failure<PhotoDto>(UserErrors.InvalidCredentials);

 user.Photo = photo;

            if (!await userRepo.SaveChangesAsync())
                return Result.Failure<PhotoDto>(PhotoErrors.PhotoNotSave);

            return Result.Success(photo.Adapt<PhotoDto>());

        }


    }
}
