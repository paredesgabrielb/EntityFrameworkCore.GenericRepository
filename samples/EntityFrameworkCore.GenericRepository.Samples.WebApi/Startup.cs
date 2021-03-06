﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.Samples.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            
            services.AddTransient<GenericRepositoryContext, SampleDbContext>();
            services.AddTransient(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));

            services.AddAutoMapper(config => config.AddCollectionMappers(), typeof(Startup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}