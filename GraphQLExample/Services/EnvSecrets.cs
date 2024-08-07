namespace GraphQLExample.Services
{
    public class EnvSecrets
    {
        public string SendGridKey { get; set; }
        public string FromEmail { get; set; }
        public string ConnectionString { get; set; }

        public EnvSecrets() 
        {
            var key = Environment.GetEnvironmentVariable("X1_SendGridKey", EnvironmentVariableTarget.User);
            var fromEmail = Environment.GetEnvironmentVariable("X1_FromEmail", EnvironmentVariableTarget.User);
            var connString = Environment.GetEnvironmentVariable("X1_ConnectionString", EnvironmentVariableTarget.User);
            
            if (string.IsNullOrEmpty(key)) throw new Exception($"SendGridKey is missing from the Environment");
            else SendGridKey = key;
            if (string.IsNullOrEmpty(fromEmail)) throw new Exception($"FromEmail is missing from the Environment");
            else FromEmail = fromEmail;
            if (string.IsNullOrEmpty(connString)) throw new ArgumentException($"connectionString is missing from the Environment");
            else ConnectionString = connString;
        }
    }
}
