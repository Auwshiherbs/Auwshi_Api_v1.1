using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services.Interface
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<byte[]> GetFileAsync(string keyName);
        Task DeleteFileAsync(string keyName);
    }
}
