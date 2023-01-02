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
using System.Linq;

namespace LittleBlocks.Reports
{
    public class ReportTemplateSelector : IReportTemplateSelector
    {
        private readonly IEnumerable<IReportTemplateProvider> _templateProviders;

        public ReportTemplateSelector(IEnumerable<IReportTemplateProvider> templateProviders)
        {
            _templateProviders = templateProviders ?? throw new ArgumentNullException(nameof(templateProviders));
        }

        public IReportTemplateProvider GetTemplate(string reportType)
        {
            if (reportType == null) throw new ArgumentNullException(nameof(reportType));

            return _templateProviders.FirstOrDefault(m => m.ReportType == reportType);
        }
    }
}