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
using LittleBlocks.Testing;
using FluentAssertions;
using Xunit;

namespace LittleBlocks.Reports.UnitTests
{
    public class ReportTemplateSelectorTests
    {
        [Theory]
        [AutoSubstituteAndData]
        public async Task Should_ReportTemplateSelector_throw_proper_error_on_wrong_configuration()
        {
            // Given

            // When
            var exception = Assert.Throws<ArgumentNullException>(() => new ReportTemplateSelector(null));

            // Then
            exception.Should().NotBeNull();
            exception.ParamName.Should().Be("templateProviders");

            await Task.CompletedTask;
        }

        [Theory]
        [AutoSubstituteAndData]
        public void Should_GetTemplate_throw_proper_error_on_null_input(
            ReportTemplateSelector sut)
        {
            // Given

            // When
            var exception = Assert.Throws<ArgumentNullException>(() => sut.GetTemplate(null));

            // Then
            exception.Should().NotBeNull();
            exception.ParamName.Should().Be("reportType");
        }

        [Theory]
        [AutoSubstituteAndData]
        public void Should_GetTemplate_return_the_right_template(
            List<IReportTemplateProvider> templateProviders)
        {
            // Given
            var expectedType = templateProviders[0].ReportType;
            var sut = new ReportTemplateSelector(templateProviders);

            // When
            var template = sut.GetTemplate(expectedType);

            // Then
            template.Should().NotBeNull();
            template.Should().BeEquivalentTo(templateProviders[0]);
        }

        [Theory]
        [AutoSubstituteAndData]
        public void Should_GetTemplate_return_null_if_the_template_is_not_found(
            List<IReportTemplateProvider> templateProviders)
        {
            // Given
            var sut = new ReportTemplateSelector(templateProviders);

            // When
            var template = sut.GetTemplate("SampleTemplate");

            // Then
            template.Should().BeNull();
        }
    }
}