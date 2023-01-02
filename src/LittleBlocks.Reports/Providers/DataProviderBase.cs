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
using System.Threading.Tasks;
using LittleBlocks.Reports.Model;

namespace LittleBlocks.Reports.Providers
{
    public abstract class DataProviderBase<TRequest> : IDataProvider<TRequest> where TRequest : ReportRequest
    {
        protected DataProviderBase(string name, string snapshotType)
        {
            Name = name;
            SnapshotType = snapshotType;
        }

        public string Name { get; }
        public string SnapshotType { get; }

        public async Task<SnapshotCollection> LoadSnapshotDataAsync(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var snapshots = await LoadSnapshotsFromDataSourceAsync(request);

            return CreateCollectionFromSnapshots(snapshots);
        }

        protected virtual SnapshotCollection CreateCollectionFromSnapshots(IReadOnlyList<Snapshot> snapshots)
        {
            return new SnapshotCollection(SnapshotType, snapshots);
        }

        protected abstract Task<IReadOnlyList<Snapshot>> LoadSnapshotsFromDataSourceAsync(TRequest request);
    }
}