﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file);
    }
}
