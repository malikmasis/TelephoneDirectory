using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TelephoneDirectory.Report.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection AddCustomOpenTelemetry(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddOpenTelemetry()
                   .WithTracing(tracingProviderBuilder => tracingProviderBuilder
                                                          .AddAspNetCoreInstrumentation()
                                                          .AddHttpClientInstrumentation()
                                                          .AddConsoleExporter()
                                                          .AddJaegerExporter()
                                                          .AddZipkinExporter()
                                                          .AddSource("Tracing.NET")
                                                          .SetResourceBuilder(
                                                              ResourceBuilder.CreateDefault()
                                                                             .AddService(serviceName: "Tracing.NET")))
                   .WithMetrics(metricsProviderBuilder => metricsProviderBuilder
                                                          .AddConsoleExporter()
                                                          .AddAspNetCoreInstrumentation()
                                                          .AddHttpClientInstrumentation()
                                                          .AddRuntimeInstrumentation()
                                                          .AddPrometheusExporter()
                                                          .AddMeter("Metrics.NET")
                                                          .AddView(
                                                              instrumentName: "components-per-order",
                                                              new ExplicitBucketHistogramConfiguration
                                                              {
                                                                  Boundaries = new double[] { 1, 2, 5, 10 }
                                                              }))
                   .StartWithHost();

            return serviceCollection;
        }
    }
}

