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
using System.Linq;
using System.Threading.Tasks;
using LittleBlocks.Excel;
using LittleBlocks.Excel.Extensions;
using LittleBlocks.Reports.Model;
using Microsoft.Extensions.Logging;

namespace LittleBlocks.Reports.Renderers.Excel
{
    public abstract class SnapshotDataRenderer<T> : ExcelReportDataRenderer where T : Snapshot
    {
        private readonly ILogger _logger;

        protected SnapshotDataRenderer(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract string SnapshotType { get; }

        public override Task RenderDataAsync(ReportData report, IWorkbook workbook)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));
            if (workbook == null) throw new ArgumentNullException(nameof(workbook));

            var collection = GetRelevantSnapshots(report);
            var snapshots = collection?.Items.OfType<T>().ToArray();

            RenderDataInternal(workbook, snapshots);

            return Task.FromResult(0);
        }

        protected abstract void RenderDataInternal(IWorkbook workbook, T[] snapshots);

        protected virtual SnapshotCollection GetRelevantSnapshots(ReportData reportData)
        {
            if (reportData == null) throw new ArgumentNullException(nameof(reportData));

            return reportData.SnapshotCollections.FirstOrDefault(s => s.Type == SnapshotType);
        }

        protected virtual void RenderFromSingleFormatter(IWorkbook workbook, IExcelFormatter<T> formatter,
            T[] snapshots)
        {
            if (workbook == null) throw new ArgumentNullException(nameof(workbook));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (snapshots == null) throw new ArgumentNullException(nameof(snapshots));
            try
            {
                var section = formatter.Format(snapshots);

                workbook.SaveInDataSheetRange(SheetName, section.HeaderRange, section.Headers);
                workbook.SaveInDataSheetRange(SheetName, section.DataRange, section.Data);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in rendering formatter {formatter.GetType().Name}", e);
                throw;
            }
        }
    }
}