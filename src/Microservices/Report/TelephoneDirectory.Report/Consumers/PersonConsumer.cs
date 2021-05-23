using System;
using System.Threading.Tasks;
using MassTransit;
using TelephoneDirectory.Guide.Entities;
using TelephoneDirectory.Report.Interfaces;

namespace TelephoneDirectory.Report.Consumers
{
    public sealed class PersonConsumer : IConsumer<Person>
    {
        private readonly IReportService _reportService;
        public PersonConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<Person> context)
        {
            Person data = context.Message;
            if (data == null)
            {
                throw new InvalidOperationException("The person was not valid");
            }
            await _reportService.Save();
        }
    }
}
