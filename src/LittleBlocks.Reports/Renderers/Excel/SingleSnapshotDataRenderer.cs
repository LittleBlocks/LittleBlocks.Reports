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
using LittleBlocks.Excel;
using LittleBlocks.Reports.Model;
using Microsoft.Extensions.Logging;

namespace LittleBlocks.Reports.Renderers.Excel
{
    public abstract class SingleSnapshotDataRenderer<T> : SnapshotDataRenderer<T> where T : Snapshot
    {
        private readonly IExcelFormatter<T> _formatter;

        protected SingleSnapshotDataRenderer(IExcelFormatter<T> formatter, ILogger logger) : base(logger)
        {
            _formatter = formatter;
        }

        protected override void RenderDataInternal(IWorkbook workbook, T[] snapshots)
        {
            if (workbook == null) throw new ArgumentNullException(nameof(workbook));
            if (snapshots == null) throw new ArgumentNullException(nameof(snapshots));

            RenderFromSingleFormatter(workbook, _formatter, snapshots);
        }
    }
}