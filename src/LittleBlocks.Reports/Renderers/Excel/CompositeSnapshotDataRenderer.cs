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
using System.Collections.Generic;
using LittleBlocks.Excel;
using LittleBlocks.Reports.Model;
using Microsoft.Extensions.Logging;

namespace LittleBlocks.Reports.Renderers.Excel
{
    public abstract class CompositeSnapshotDataRenderer<T> : SnapshotDataRenderer<T> where T : Snapshot
    {
        private readonly IEnumerable<IExcelFormatter<T>> _formatters;

        protected CompositeSnapshotDataRenderer(IEnumerable<IExcelFormatter<T>> formatters, ILogger logger) :
            base(logger)
        {
            _formatters = formatters ?? throw new ArgumentNullException(nameof(formatters));
        }

        protected override void RenderDataInternal(IWorkbook workbook, T[] snapshots)
        {
            if (workbook == null) throw new ArgumentNullException(nameof(workbook));
            if (snapshots == null) throw new ArgumentNullException(nameof(snapshots));

            foreach (var formatter in _formatters) RenderFromSingleFormatter(workbook, formatter, snapshots);
        }
    }
}