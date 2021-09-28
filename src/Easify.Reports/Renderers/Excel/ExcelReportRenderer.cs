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
using System.Collections.Generic;
using System.Threading.Tasks;
using Easify.Excel;
using Easify.Excel.Extensions;
using Easify.Reports.Model;
using Easify.Reports.Providers.Exceptions;

namespace Easify.Reports.Renderers.Excel
{
    public class ExcelReportRenderer : IReportRenderer
    {
        private readonly IEnumerable<IExcelReportDataRenderer> _excelReportDataRenderers;
        private readonly IReportTemplateSelector _reportTemplateSelector;
        private readonly IWorkbookLoader _workbookLoader;

        public ExcelReportRenderer(
            IWorkbookLoader workbookLoader,
            IEnumerable<IExcelReportDataRenderer> excelReportDataRenderers,
            IReportTemplateSelector reportTemplateSelector)
        {
            _excelReportDataRenderers = excelReportDataRenderers ??
                                        throw new ArgumentNullException(nameof(excelReportDataRenderers));
            _reportTemplateSelector =
                reportTemplateSelector ?? throw new ArgumentNullException(nameof(reportTemplateSelector));
            _workbookLoader = workbookLoader ?? throw new ArgumentNullException(nameof(workbookLoader));
        }

        public RendererType Type => RendererType.Excel;

        public async Task<ReportOutputStream> RenderAsync(ReportData report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var templateProvider = _reportTemplateSelector.GetTemplate(report.ReportType);
            if (templateProvider == null)
                throw new ReportGenerationException($"Template with type {report.ReportType} is not found");

            var workbook = _workbookLoader.Load(templateProvider.GetTemplatePath());

            foreach (var dataRenderer in _excelReportDataRenderers)
                await dataRenderer.RenderDataAsync(report, workbook);

            var stream = workbook.ToMemoryStream();

            return new ReportOutputStream
            {
                Stream = stream,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = templateProvider.GetFileName(report),
                ContentDisposition = "attachment"
            };
        }
    }
}