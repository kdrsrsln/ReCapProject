﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        
        IDataResult<List<Car>> ICarService.GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }
        IDataResult<List<Car>> ICarService.GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }
        IDataResult<List<Car>> ICarService.GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }
        public IDataResult<List<CarDetailDto>> GetAllCarDetailDtos()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetailDtos());
        }

        public IDataResult<CarDetailDto> GetCarDetail(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetAllCarDetailDtos(c=> c.Id == carId).FirstOrDefault());
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetailByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetailDtos(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetailByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetailDtos(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetailByBrandAndColor(int brandId, int colorId)
        {
            var result = _carDal.GetAllCarDetailDtos(c => c.BrandId == brandId && c.ColorId == colorId);
            if (result.Any())
            {
                return new SuccessDataResult<List<CarDetailDto>>(result);
            }
            else
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NoResultForThisFilter);
            }
            
        }

        public IResult Add(Car car)
        {
            if (car.Model.Length >2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }
    }
}
