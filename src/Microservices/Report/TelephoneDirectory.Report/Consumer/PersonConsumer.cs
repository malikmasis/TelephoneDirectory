using MassTransit;
using System.Threading.Tasks;
using TelephoneDirectory.Guide.Entities;

namespace TelephoneDirectory.Report.Consumer
{
    public class PersonConsumer : IConsumer<Person>
    {
        public async Task Consume(ConsumeContext<Person> context)
        {
            var data = context.Message;
            //save into the db or queue
        }
    }
}
