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
using System.IO;
using System.Threading.Tasks;
using LittleBlocks.Reports.Model;
using LittleBlocks.Reports.Renderers.Json;
using LittleBlocks.Testing;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace LittleBlocks.Reports.UnitTests.Renderers.Json
{
    public class JsonReportRendererTests
    {
        [Theory]
        [AutoSubstituteAndData]
        public async Task Should_have_a_correct_configuration(
            ReportData report,
            JsonReportRenderer sut)
        {
            // Given

            // When
            var result = await sut.RenderAsync(report);

            // Then
            sut.Type.Should().Be(RendererType.Json);
            result.Should().NotBeNull();
            result.ContentDisposition.Should().Be("inline");
            result.ContentType.Should().Be("application/json");
            result.FileName.Should().Be("data.json");
            result.Stream.Should().NotBeNull();
        }

        [Theory]
        [AutoSubstituteAndData]
        public async Task Should_RenderAsync_return_a_valid_stream(
            List<SampleSnapshot> snapshots,
            JsonReportRenderer sut)
        {
            // Given
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.All
            };
            var report = new ReportData
            {
                SnapshotCollections = new List<SnapshotCollection>
                {
                    new SnapshotCollection("SnapshotType", snapshots)
                }
            };

            // When
            var result = await sut.RenderAsync(report);

            var sr = new StreamReader(result.Stream);
            var json = await sr.ReadToEndAsync();
            var reportData = JsonConvert.DeserializeObject<ReportData>(json, settings);

            // Then
            reportData.Should().NotBeNull();
            reportData.Should().BeEquivalentTo(report);
        }
    }

    public class SampleSnapshot : Snapshot
    {
        public string Name { get; set; }
    }
}