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


using System.Collections.Generic;
using LittleBlocks.Reports.Model;

namespace LittleBlocks.Reports.Renderers.Excel
{
    public abstract class GroupExcelFormatter<T> : IExcelFormatter<T> where T : Snapshot, new()
    {
        public abstract string SectionHeader { get; }
        public abstract string SectionData { get; }

        public virtual SectionData Format(T[] data)
        {
            if (data == null) return new SectionData { HeaderRange = SectionHeader, DataRange = SectionData };

            return new SectionData
            {
                HeaderRange = SectionHeader,
                Headers = GetGroupedHeaders(data),
                DataRange = SectionData,
                Data = GetGroupedProperties(data)
            };
        }

        protected abstract List<object[]> GetGroupedProperties(T[] ts);
        protected abstract List<object[]> GetGroupedHeaders(T[] ts);
    }
}