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
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Easify.Reports.Model;
using Easify.Reports.Renderers;
using Easify.Testing;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Easify.Reports.UnitTests
{
    public class ReportGeneratorTests
    {
        [Theory]
        [AutoSubstituteAndData]
        public async Task Should_GenerateReportAsync_throw_proper_error_on_wront_configuration(
            IReportBuilder<ReportRequest> builder)
        {
            // Given

            // When
            var exception1 = Assert.Throws<ArgumentNullException>(() => new SampleReportGenerator(null, null));
            var exception2 =
                Assert.Throws<ArgumentNullException>(() => new SampleReportGenerator(builder, null));

            // Then
            exception1.Should().NotBeNull();
            exception1.ParamName.Should().Be("reportBuilder");
            exception2.Should().NotBeNull();
            exception2.ParamName.Should().Be("reportRendererFactory");

            await Task.CompletedTask;
        }

        [Theory]
        [AutoSubstituteAndData]
        public async Task Should_ReportGenerator_throw_proper_error_on_null_input(
            SampleReportGenerator sut)
        {
            // Given
            SampleReportRequest request = null;

            // When
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GenerateReportAsync(request));

            // Then
            exception.Should().NotBeNull();
            exception.ParamName.Should().Be(nameof(request));
        }

        [Theory]
        [AutoSubstituteAndData]
        public async Task Should_GenerateReportAsync_generate_the_right_data_for_report(
            ReportOutputStream outputStream,
            SampleReportRequest request,
            ReportData reportData,
            IReportRenderer renderer,
            [Frozen] IReportBuilder<SampleReportRequest> builder,
            [Frozen] IReportRendererFactory factory,
            SampleReportGenerator sut)
        {
            // Given
            builder.GenerateReportDataAsync(request).Returns(Task.FromResult(reportData));
            factory.Create(Arg.Any<RendererType>()).Returns(renderer);
            renderer.RenderAsync(Arg.Any<ReportData>()).Returns(Task.FromResult(outputStream));

            // When
            var output = await sut.GenerateReportAsync(request);

            // Then
            output.Should().NotBeNull();
            output.Should().BeEquivalentTo(outputStream);
        }
    }

    public class SampleReportGenerator : ReportGenerator<SampleReportRequest>
    {
        public SampleReportGenerator(IReportBuilder<SampleReportRequest> reportBuilder,
            IReportRendererFactory reportRendererFactory) : base(reportBuilder, reportRendererFactory)
        {
        }
    }

    public class SampleReportRequest : ReportRequest
    {
    }
}