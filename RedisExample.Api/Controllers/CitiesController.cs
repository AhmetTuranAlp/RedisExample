using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisExample.Api.Common;
using RedisExample.Api.Model;
using RedisExample.Cache;

namespace RedisExample.Api.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {

        /// <summary>
        /// Default ülke için şehirler getirilmektedir.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ListResponse<City> GetCountry()
        {
            try
            {
                string countryId = "90";
                var redisManager = new RedisCacheManager();
                List<City> cities = redisManager.GetAll<City>(countryId);
                if (cities != null)
                    return new ListResponse<City>() { IsSuccess = true, Message = "Transaction Successful.", Result = cities, TotalCount = cities.Count(), Exception = null };
                else
                {
                    cities = GetCityList(countryId);
                    if (cities.Count > 0)
                    {
                        redisManager.Set(countryId, cities, 60);
                        return new ListResponse<City>() { IsSuccess = true, Message = "Transaction Successful", Result = cities, TotalCount = cities.Count(), Exception = null };
                    }
                    else
                    {
                        return new ListResponse<City>() { IsSuccess = false, Message = "No Registration For This Country", Result = new List<City>(), TotalCount = 0, Exception = null };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ListResponse<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new List<City>(), TotalCount = 0, Exception = ex };
            }
        }

        /// <summary>
        /// Ülke kodu ile şehirler getirilmektedir.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet, Route("[action]/countryId={countryId}")]
        public ListResponse<City> GetCountry(string countryId)
        {
            try
            {
                if (!string.IsNullOrEmpty(countryId))
                {
                    var redisManager = new RedisCacheManager();
                    List<City> cities = redisManager.GetAll<City>(countryId);
                    if (cities != null)
                        return new ListResponse<City>() { IsSuccess = true, Message = "Transaction Successful.", Result = cities, TotalCount = cities.Count(), Exception = null };
                    else
                        return new ListResponse<City>() { IsSuccess = false, Message = "No Registration For This Country.", Result = new List<City>(), TotalCount = 0, Exception = null };
                }
                else
                {
                    return new ListResponse<City>() { IsSuccess = false, Message = "Parameter Null", Result = new List<City>(), TotalCount = 0, Exception = null };
                }
            }
            catch (Exception ex)
            {
                return new ListResponse<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new List<City>(), TotalCount = 0, Exception = ex };
            }
        }

        /// <summary>
        /// Ülke kodu ve şehir kodu ile şehir getirilmektedir.
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet, Route("[action]/countryId={countryId}&cityId={cityId}")]
        public Response<City> GetCity(string countryId, string cityId)
        {
            try
            {
                if (!string.IsNullOrEmpty(countryId) && !string.IsNullOrEmpty(cityId))
                {
                    var redisManager = new RedisCacheManager();
                    List<City> cities = redisManager.GetAll<City>(countryId);
                    if (cities != null)
                    {
                        City city = cities.FirstOrDefault(x => x.CityId == Convert.ToInt32(cityId));
                        if (city != null)
                            return new Response<City>() { IsSuccess = true, Message = "Transaction Successful.", Result = city, Exception = null };
                        else
                            return new Response<City>() { IsSuccess = false, Message = "No Registration For This City.", Result = new City(), Exception = null };
                    }
                    else
                    {
                        return new Response<City>() { IsSuccess = false, Message = "Parameter Null", Result = new City(), Exception = null };
                    }
                }
                else
                {
                    return new Response<City>() { IsSuccess = false, Message = "Parameter Null", Result = new City(), Exception = null };
                }
            }
            catch (Exception ex)
            {
                return new Response<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new City(), Exception = ex };
            }
        }

        /// <summary>
        /// Ülke kodu ve şehir modeli ile şehir eklemesi yapılmaktadır.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        public ListResponse<City> SetCity([FromBody]City city, string countryId)
        {
            try
            {
                if (city != null && !string.IsNullOrEmpty(countryId))
                {
                    var redisManager = new RedisCacheManager();
                    List<City> cities = redisManager.GetAll<City>(countryId);
                    if (cities == null)
                        cities = new List<City>();

                    cities.Add(city);

                    redisManager.Set(countryId, cities, 60);
                    return new ListResponse<City>() { IsSuccess = true, Message = "Transaction Successful", Result = cities, TotalCount = cities.Count(), Exception = null };
                }
                else
                {
                    return new ListResponse<City>() { IsSuccess = false, Message = "Parameter Null", Result = new List<City>(), TotalCount = 0, Exception = null };
                }
            }
            catch (Exception ex)
            {
                return new ListResponse<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new List<City>(), TotalCount = 0, Exception = ex };
            }
        }
        
        /// <summary>
        /// Ülke koduna göre şehirleri dönmektedir.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public List<City> GetCityList(string countryId)
        {
            List<City> cities = new List<City>();
            if (countryId == "90")
            {
                cities.Add(new City { CityId = 34, CityName = "İstanbul" });
                cities.Add(new City { CityId = 58, CityName = "Sivas" });
                cities.Add(new City { CityId = 54, CityName = "Sakarya" });
                cities.Add(new City { CityId = 06, CityName = "Ankara" });
            }
            return cities;
        }


    }
}