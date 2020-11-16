using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Controllers
{
    public interface ICqController
    {
        public void Process(JToken token);
    }
}
