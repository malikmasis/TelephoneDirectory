using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Entities;
using TelephoneDirectory.Report.IntegrationTest.Base;
using Xunit;

namespace TelephoneDirectory.Report.IntegrationTest;

public sealed class ReportControllerTest : BaseTest
{
    public ReportControllerTest(TestAuthFactory<TestStartup> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_all_reports()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/Report/getall");
        Assert.True(response.IsSuccessStatusCode);

        var body = await response.Content.ReadAsStringAsync();
        var reply = JsonSerializer.Deserialize<List<ReportOutput>>(body);
        Assert.NotNull(reply);
    }

    [Fact]
    public async Task Get_report_by_id()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/Report/get/1");
        Assert.True(response.IsSuccessStatusCode);

        var body = await response.Content.ReadAsStringAsync();
        var reply = JsonSerializer.Deserialize<ReportOutput>(body);
        Assert.NotNull(reply);
    }
}
