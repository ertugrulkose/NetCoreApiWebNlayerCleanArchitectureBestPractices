namespace CleanApp.API.Extensions
{
    public static class ConfigurePipelineExtensions
    {
        public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
        {
            app.UseExceptionHandler(x => { });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerExt();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            return app;
        }
    }
}
