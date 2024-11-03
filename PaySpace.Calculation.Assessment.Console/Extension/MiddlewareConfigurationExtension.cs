using Microsoft.AspNetCore.Builder;
using PaySpace.Calculation.Assessment.Console.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Extension
{
    internal static class MiddlewareConfigurationExtension
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            return app;
        }
    }
}
