namespace ProjectManagement.ConfigureApp
{
    public static class AppConfigure
    {
        public static void Configure(WebApplication app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.MapControllers();
        }
    }
}
