namespace Sivilab.API.Services
{
    public class MockEmailService : IEmailService
    {
        private readonly ILogger<MockEmailService> _logger;

        public MockEmailService(ILogger<MockEmailService> logger)
        {
            _logger = logger;
        }

        public Task<bool> EnviarCodigoVerificacion(string destinatario, string nombreCompleto, string codigo)
        {
            _logger.LogInformation("📧 MOCK EMAIL - Código de Verificación");
            _logger.LogInformation("   Destinatario: {Email}", destinatario);
            _logger.LogInformation("   Nombre: {Nombre}", nombreCompleto);
            _logger.LogInformation("   Código: {Codigo}", codigo);
            
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     EMAIL SIMULADO - VERIFICACIÓN      ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine($"║ Para:   {destinatario,-30} ║");
            Console.WriteLine($"║ Nombre: {nombreCompleto,-30} ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine($"║ CÓDIGO: {codigo,-30} ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");
            
            return Task.FromResult(true);
        }

        public Task<bool> EnviarCodigoRecuperacion(string destinatario, string nombreCompleto, string codigo)
        {
            _logger.LogInformation("📧 MOCK EMAIL - Código de Recuperación");
            _logger.LogInformation("   Código: {Codigo}", codigo);
            
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     EMAIL SIMULADO - RECUPERACIÓN      ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine($"║ Para:   {destinatario,-30} ║");
            Console.WriteLine($"║ CÓDIGO: {codigo,-30} ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");
            
            return Task.FromResult(true);
        }

        public Task<bool> EnviarCorreoBienvenida(string destinatario, string nombreCompleto)
        {
            _logger.LogInformation("📧 MOCK EMAIL - Bienvenida");
            _logger.LogInformation("   Para: {Email}", destinatario);
            
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     EMAIL SIMULADO - BIENVENIDA        ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine($"║ Para:   {destinatario,-30} ║");
            Console.WriteLine($"║ Nombre: {nombreCompleto,-30} ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");
            
            return Task.FromResult(true);
        }
    }
}