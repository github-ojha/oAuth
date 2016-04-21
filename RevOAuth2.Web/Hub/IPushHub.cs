using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevOAuth2.Web.Hub
{
    public interface IPushHub
    {
        string ConnectionId { get; set; }
        void SendError(string message);
        void SendConfirmation(string message);
       

    }
}
