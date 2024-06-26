﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

#if NETCORE
using Microsoft.AspNetCore.Http;
using Moesif.Middleware.NetCore;
#endif

#if NET461
using Microsoft.Owin;
using Moesif.Middleware.NetFramework;
#endif

class MyILoggerFactory : ILoggerFactory
{
    public void AddProvider(ILoggerProvider provider)
    {
        ;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MyILogger();
    }

    public void Dispose()
    {
        ;
    }
};

class MyILogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        ;
    }
};

#if NETCORE
namespace Moesif.Middleware
{
    public class MoesifMiddleware
    {
        MoesifMiddlewareNetCore netCoreMoesifMiddleware;

        public MoesifMiddleware(RequestDelegate next, Dictionary<string, object> _middleware, ILoggerFactory logger)
        {
            netCoreMoesifMiddleware = new MoesifMiddlewareNetCore(next, _middleware, logger);
        }

        public MoesifMiddleware(Dictionary<string, object> _middleware) => netCoreMoesifMiddleware = new MoesifMiddlewareNetCore(_middleware);

        public async Task Invoke(HttpContext httpContext) 
        {
            try
            {
                await netCoreMoesifMiddleware.Invoke(httpContext);
            }
            catch 
            { }
        }

        // Function to update user
        public void UpdateUser(Dictionary<string, object> userProfile)
        {
            netCoreMoesifMiddleware.UpdateUser(userProfile);
        }

        // Function to update users in batch
        public void UpdateUsersBatch(List<Dictionary<string, object>> userProfiles)
        {
            netCoreMoesifMiddleware.UpdateUsersBatch(userProfiles);
        }

        // Function to update company
        public void UpdateCompany(Dictionary<string, object> companyProfile)
        {
            netCoreMoesifMiddleware.UpdateCompany(companyProfile);
        }

        // Function to update companies in batch
        public void UpdateCompaniesBatch(List<Dictionary<string, object>> companyProfiles)
        {
            netCoreMoesifMiddleware.UpdateCompaniesBatch(companyProfiles);
        }
    }
}
#endif

#if NET461
namespace Moesif.Middleware
{
    public class MoesifMiddleware: OwinMiddleware
    {
        MoesifMiddlewareNetFramework netFrameworkMoesifMiddleware;

        public MoesifMiddleware(OwinMiddleware next, Dictionary<string, object> _middleware, ILoggerFactory logger) : base(next)
        {
            netFrameworkMoesifMiddleware = new MoesifMiddlewareNetFramework(next, _middleware, logger);
        }

        public MoesifMiddleware(OwinMiddleware next, Dictionary<string, object> _middleware) : base(next)
        {
            ILoggerFactory logger = new MyILoggerFactory();

            netFrameworkMoesifMiddleware = new MoesifMiddlewareNetFramework(next, _middleware, logger);
        }

        public MoesifMiddleware(Dictionary<string, object> _middleware) : base(null)
        {
            netFrameworkMoesifMiddleware = new MoesifMiddlewareNetFramework(_middleware);
        }

        public async override Task Invoke(IOwinContext httpContext)
        {
            try 
            {
                await netFrameworkMoesifMiddleware.Invoke(httpContext);
            }
            catch 
            { }
        }

        // Function to update user
        public void UpdateUser(Dictionary<string, object> userProfile)
        {
            netFrameworkMoesifMiddleware.UpdateUser(userProfile);
        }

        // Function to update users in batch
        public void UpdateUsersBatch(List<Dictionary<string, object>> userProfiles)
        {
            netFrameworkMoesifMiddleware.UpdateUsersBatch(userProfiles);
        }

        // Function to update company
        public void UpdateCompany(Dictionary<string, object> companyProfile)
        {
            netFrameworkMoesifMiddleware.UpdateCompany(companyProfile);
        }

        // Function to update companies in batch
        public void UpdateCompaniesBatch(List<Dictionary<string, object>> companyProfiles)
        {
            netFrameworkMoesifMiddleware.UpdateCompaniesBatch(companyProfiles);
        }
    }
}
#endif
