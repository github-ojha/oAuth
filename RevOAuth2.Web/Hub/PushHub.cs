using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;

namespace RevOAuth2.Web.Hub
{
    /// <summary>
    /// Client Hub to notify UI
    /// Will used in validation part
    /// </summary>    
    public class PushHub : Microsoft.AspNet.SignalR.Hub , IPushHub
    {
        
        private string _connectionId;

        
        public void SendError(string message)
        {           
            if (!string.IsNullOrEmpty(ConnectionId))
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
				context.Clients.Client(ConnectionId).revOAuthError(message);
            }
        }

        public void SendConfirmation(string message)
        {
            if (!string.IsNullOrEmpty(ConnectionId))
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
				context.Clients.Client(ConnectionId).revOAuthConfirmation(message);
            }            
            //context.Clients.All.registrationConfirmation(message);  
        }

      

        public string ConnectionId
        {
            get
            {
                return _connectionId;
            }
            set
            {
                _connectionId = value;
            }
        }
    }
}