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
using System.Linq;

namespace Easify.Reports.Renderers.Excel.Extensions
{
    public static class ObjectListFormattingExtensions
    {
        public static void AddEmptyRange(this List<object> objectList, int count, string content = "")
        {
            if (objectList == null) throw new ArgumentNullException(nameof(objectList));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            var items = Enumerable.Range(1, count).Select(m => content).ToArray();
            objectList.AddRange(items);
        }
    }
}