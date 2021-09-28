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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Easify.Reports.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Easify.Reports.Renderers.Json
{
    public class JsonReportRenderer : IReportRenderer
    {
        private readonly ILogger<JsonReportRenderer> _logger;

        public JsonReportRenderer(ILogger<JsonReportRenderer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public RendererType Type => RendererType.Json;

        public Task<ReportOutputStream> RenderAsync(ReportData report)
        {
            var reportOutput = new ReportOutput { Data = report };

            var reportOutputStream = GenerateOutputStream(reportOutput);
            return Task.FromResult(reportOutputStream);
        }

        private ReportOutputStream GenerateOutputStream(ReportOutput reportOutput)
        {
            try
            {
                var stream = new MemoryStream();
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var jsonData = JsonConvert.SerializeObject(reportOutput.Data, settings);
                var buffer = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;

                var outputStream = new ReportOutputStream
                {
                    ContentDisposition = "inline",
                    ContentType = "application/json",
                    FileName = "data.json",
                    Stream = stream
                };

                return outputStream;
            }
            catch (Exception e)
            {
                _logger.LogError("Error in saving the json output", e);
                throw;
            }
        }
    }
}