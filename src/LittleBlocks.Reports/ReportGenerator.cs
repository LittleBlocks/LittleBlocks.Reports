// This software is part of the LittleBlocks.Reports Library
// Copyright (C) 2021 LittleBlocks
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
using System.Threading.Tasks;
using LittleBlocks.Reports.Model;
using LittleBlocks.Reports.Renderers;

namespace LittleBlocks.Reports
{
    public abstract class ReportGenerator<T> : IReportGenerator<T> where T : ReportRequest
    {
        private readonly IReportBuilder<T> _reportBuilder;
        private readonly IReportRendererFactory _reportRendererFactory;

        protected ReportGenerator(IReportBuilder<T> reportBuilder, IReportRendererFactory reportRendererFactory)
        {
            _reportBuilder = reportBuilder ?? throw new ArgumentNullException(nameof(reportBuilder));
            _reportRendererFactory = reportRendererFactory ?? throw new ArgumentNullException(nameof(reportRendererFactory));
        }

        public virtual async Task<ReportOutputStream> GenerateReportAsync(T request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var report = await _reportBuilder.GenerateReportDataAsync(request);
            var reportOutput = await RenderReportAsync(report, request);

            return reportOutput;
        }

        private async Task<ReportOutputStream> RenderReportAsync(ReportData report, T request)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var renderer = _reportRendererFactory.Create(request.RendererType);

            return await renderer.RenderAsync(report);
        }
    }
}