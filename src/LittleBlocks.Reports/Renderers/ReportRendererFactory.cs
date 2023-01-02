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
using System.ComponentModel;
using System.Linq;
using LittleBlocks.Reports.Model;
using LittleBlocks.Reports.Providers.Exceptions;

namespace LittleBlocks.Reports.Renderers
{
    public class ReportRendererFactory : IReportRendererFactory
    {
        private readonly IEnumerable<IReportRenderer> _reportRenderers;

        public ReportRendererFactory(IEnumerable<IReportRenderer> reportRenderers)
        {
            _reportRenderers = reportRenderers;
        }

        public IReportRenderer Create(RendererType rendererType)
        {
            if (!Enum.IsDefined(typeof(RendererType), rendererType))
                throw new InvalidEnumArgumentException(nameof(rendererType), (int)rendererType, typeof(RendererType));

            var renderer = _reportRenderers.FirstOrDefault(r => r.Type == rendererType);
            if (renderer == null)
                throw new ReportGenerationException($"Renderer {rendererType} is not registered");

            return renderer;
        }
    }
}