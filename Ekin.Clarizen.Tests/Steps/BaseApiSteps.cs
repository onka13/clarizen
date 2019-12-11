﻿using System.Globalization;
using Ekin.Clarizen.Tests.Context;
using Microsoft.Extensions.Configuration;

namespace Ekin.Clarizen.Tests.Steps
{
    public abstract class BaseApiSteps
    {
        protected readonly IConfiguration Configuration;
        protected BaseContext Context;

        public BaseApiSteps(BaseContext context)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
            Configuration = TestHelper.GetConfiguration();
            Context = context;
        }

        protected API Api => Context.Api;

        protected void DeleteTestData()
        {
            if (Context?.Api == null)
                return;
            var query = new Ekin.Clarizen.Data.Request.query(
                "SELECT name ,state FROM project where name like 'UnitTest%' ");

            var results = Context.Api.ExecuteQuery(query).Data;
            foreach (var projectId in results.GetEntityIds())
            {
                Context.Api.DeleteObject(projectId);
            }
        }
    }
}