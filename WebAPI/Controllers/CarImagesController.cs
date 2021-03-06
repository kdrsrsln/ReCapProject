﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private ICarImageService _carImageService;
        private IWebHostEnvironment _webHostEnvironment;
        private string webRootPath;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
            webRootPath = _webHostEnvironment.WebRootPath;
        }

        [HttpGet("getCarImages")]
        public IActionResult GetAll(int carId)
        {
            var result = _carImageService.GetAll(carId);
            if (result.Success)
            {
                return Ok(result);
            }
                return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(IFormFile file,[FromForm] CarImage carImage)
        {
            var result =  _carImageService.Add(carImage, file, webRootPath);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(IFormFile file,[FromForm] CarImage carImage)
        {
            var result = _carImageService.Update(carImage, file, webRootPath);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm]CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
