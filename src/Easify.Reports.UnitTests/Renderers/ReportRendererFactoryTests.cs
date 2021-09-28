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


using System.Collections.Generic;
using Easify.Reports.Model;
using Easify.Reports.Providers.Exceptions;
using Easify.Reports.Renderers;
using Easify.Reports.Renderers.Excel;
using Easify.Reports.Renderers.Json;
using Easify.Testing;
using FluentAssertions;
using Xunit;

namespace Easify.Reports.UnitTests.Renderers
{
    public class ReportRendererFactoryTests
    {
        [Theory]
        [AutoSubstituteAndData]
        public void Should_ReportRendererFactory_have_a_correct_default_configuration(
            IEnumerable<IReportRenderer> renderers)
        {
            // Given

            // When
            var rendererFactory = new ReportRendererFactory(renderers);

            // Then
            rendererFactory.Should().NotBeNull();
        }

        [Theory]
        [AutoSubstituteAndData]
        public void Should_Create_throw_pla_exception_when_the_renderer_type_cannot_be_found(
            ExcelReportRenderer excelReportRenderer,
            JsonReportRenderer jsonReportRenderer)
        {
            // Given
            var renderers = new List<IReportRenderer> { excelReportRenderer, jsonReportRenderer };
            var sut = new ReportRendererFactory(renderers);

            // When
            var result = Assert.Throws<ReportGenerationException>(() => sut.Create(RendererType.Pdf));

            // Then
            result.Should().NotBeNull();
            result.Message.Should().Be($"Renderer {RendererType.Pdf} is not registered");
        }

        [Theory]
        [AutoSubstituteAndData]
        public void Should_Create_return_a_valid_excel_renderer(
            ExcelReportRenderer excelReportRenderer,
            JsonReportRenderer jsonReportRenderer)
        {
            // Given
            var renderers = new List<IReportRenderer> { excelReportRenderer, jsonReportRenderer };
            var sut = new ReportRendererFactory(renderers);

            // When
            var result = sut.Create(RendererType.Excel);

            // Then
            result.Should().NotBeNull();
            result.Type.Should().Be(RendererType.Excel);
        }

        [Theory]
        [AutoSubstituteAndData]
        public void Should_Create_return_a_valid_json_renderer(
            ExcelReportRenderer excelReportRenderer,
            JsonReportRenderer jsonReportRenderer)
        {
            // Given
            var renderers = new List<IReportRenderer> { excelReportRenderer, jsonReportRenderer };
            var sut = new ReportRendererFactory(renderers);

            // When
            var result = sut.Create(RendererType.Json);

            // Then
            result.Should().NotBeNull();
            result.Type.Should().Be(RendererType.Json);
        }
    }
}