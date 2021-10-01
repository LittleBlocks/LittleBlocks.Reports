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
using Easify.Reports.Model;
using Easify.Reports.Providers.Exceptions;

namespace Easify.Reports.Providers
{
    public abstract class DataProvidersResolver<TRequest> : IDataProvidersResolver<TRequest>
        where TRequest : ReportRequest
    {
        private readonly IEnumerable<IDataProvider<TRequest>> _dataProviders;

        protected DataProvidersResolver(IEnumerable<IDataProvider<TRequest>> dataProviders)
        {
            _dataProviders = dataProviders ?? throw new ArgumentNullException(nameof(dataProviders));
        }

        public IEnumerable<IDataProvider<TRequest>> GetDataProviders()
        {
            var snapshotTypes = GetSnapshotTypes();
            if (snapshotTypes == null)
                throw new InvalidSnapshotListException("Invalid list of snapshot types");

            var providers = _dataProviders.Where(dp => snapshotTypes.Contains(dp.SnapshotType)).ToList();
            var missingTypes = snapshotTypes.Except(providers.Select(p => p.SnapshotType).ToArray()).ToArray();
            if (missingTypes.Any())
                throw new DataProviderMissingException(missingTypes);

            return providers;
        }

        protected abstract string[] GetSnapshotTypes();
    }
}