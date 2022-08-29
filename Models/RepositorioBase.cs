namespace InmobiliariaULP.Models;

public abstract class RepositorioBase
{
     protected readonly IConfiguration configuration;
        protected readonly string connectionString;

        protected RepositorioBase(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["Server=localhost;User=root;Password=;Database=test;SslMode=none"];
        }
}

