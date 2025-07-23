using Api.Modules.Logs.Domain;

namespace Api.Modules.Logs.Infrastructure.Repositories
{
    public class LogRepository(List<Log> logs)
    {
        public void Create(Log log)
        {
            logs.Add(log);
        }
        public List<Log> GetAll()
        {
            return logs;
        }
    }
}
