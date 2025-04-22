using Application.Business.Common.Const;
using Application.Business.Model.Cache;
using Application.Business.Model.Response.Parameter;
using Application.Business.Service.Interface;
using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.Parameter;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.Caching;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Business.Service
{
    public class ParameterService : BaseService, IParameterService
    {
        private readonly IMapper _mapper;
        private readonly ILogManager<ParameterService> _logManager;
        private readonly IParameterDAO _parameterDAO;
        private readonly ICacheManager _cacheManager;
        public ParameterService(IMapper mapper, ILogManager<ParameterService> logManager,IParameterDAO parameterDAO,ICacheManager cacheManager)
        {
            _logManager = logManager;
            _mapper = mapper;
            _parameterDAO = parameterDAO;
            _cacheManager = cacheManager;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _parameterDAO);
        }

        public List<GetParameterBusinessResponseModel> GetParameters(int type,string group)
        {
            List<ParameterCacheModel> cacheResult = GetParametersFromCache();


            List<ParameterCacheModel> searchResult = cacheResult.Where(x => x.Type == (int)type && (string.IsNullOrEmpty(group) || x.Group.Equals(group))).ToList();

            return _mapper.Map<List<ParameterCacheModel>, List<GetParameterBusinessResponseModel>>(searchResult);
        }

        #region cache
        private List<ParameterCacheModel> GetParametersFromCache()
        {
            List<ParameterCacheModel> cacheResult = _cacheManager.GetCache<List<ParameterCacheModel>>(_cacheManager.GenerateCacheKey(CacheKeys.PARAMETER));

            //Cachede veri varsa cacheden okunur.
            if (cacheResult != null)
                return cacheResult;


            //dbden okunur
            List<ParameterDAOModel> serviceResult = _parameterDAO.GetParameters();

            List<ParameterCacheModel> cacheData = _mapper.Map<List<ParameterDAOModel>, List<ParameterCacheModel>>(serviceResult);

            _cacheManager.SetCache(_cacheManager.GenerateCacheKey(CacheKeys.PARAMETER), cacheData, _cacheManager.SetCacheEntryOptions(30, CacheTimeEnum.Minute));

            return cacheData;
        }
        #endregion
    }
}
