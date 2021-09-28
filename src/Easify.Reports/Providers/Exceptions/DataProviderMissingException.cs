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

namespace Easify.Reports.Providers.Exceptions
{
    public class DataProviderMissingException : ReportGenerationException
    {
        public DataProviderMissingException(string[] snapshotTypes) : base(RenderMessage(snapshotTypes))
        {
        }

        public DataProviderMissingException(string[] snapshotTypes, Exception innerException) : base(
            RenderMessage(snapshotTypes), innerException)
        {
        }

        public static string RenderMessage(string[] snapshotTypes)
        {
            if (snapshotTypes == null) throw new ArgumentNullException(nameof(snapshotTypes));
            var types = string.Join(",", snapshotTypes);
            if (string.IsNullOrWhiteSpace(types))
                throw new ReportGenerationException(
                    "Render faild due to empty set of snapshots in DataProviderMissingException");

            return $"Can not find providers for {types}";
        }
    }
}