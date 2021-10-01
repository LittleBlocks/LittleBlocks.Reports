// This software is part of the Easify.Reports Library
// Copyright (C) 2021 Intermediate Capital Group
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 


using System;
using Easify.Excel;
using Easify.Excel.ClosedXml;
using Easify.Reports.Renderers;
using Easify.Reports.Renderers.Excel;
using Easify.Reports.Renderers.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Easify.Reports
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReporting(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Excel Reader
            services.TryAddSingleton<IWorkbookLoader, WorkbookLoader>();

            services.AddTransient<IReportRendererFactory, ReportRendererFactory>();
            services.AddSingleton<IReportTemplateSelector, ReportTemplateSelector>();
            services.AddSingleton<IReportRenderer, JsonReportRenderer>();
            services.AddSingleton<IReportRenderer, ExcelReportRenderer>();

            return services;
        }
    }
}