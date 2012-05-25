using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using log4net;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;

namespace TfsServices.Configuration
{
    public class MyTfsServer : IDisposable
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MyTfsServer));
        
        private readonly TfsConfigurationServer _tfsConfigurationServer;
        private NetworkCredential _networkCredential;

        public MyTfsServer(CiEntryPointSetting ciEntryPointSetting)
        {
            try {
                _tfsConfigurationServer = GetTfsConfigurationServer(ciEntryPointSetting.Url, ciEntryPointSetting.UserName, ciEntryPointSetting.GetPassword());
                _tfsConfigurationServer.EnsureAuthenticated();
            } catch (TeamFoundationServiceUnavailableException ex) {
                throw new ServerUnavailableException(ex.Message, ex);
            }
        }

        private TfsConfigurationServer GetTfsConfigurationServer(string url, string rawUserName, string password)
        {
            var uri = new Uri(url);
            if (string.IsNullOrEmpty(rawUserName))
            {
                return new TfsConfigurationServer(uri);
            }

            var usernameParts = rawUserName.Split('\\');
            string userName = usernameParts.LastOrDefault();
            string domain = usernameParts.FirstOrDefault();
            _networkCredential = new NetworkCredential(userName, password, domain);
            return new TfsConfigurationServer(uri, _networkCredential);
        }

        public IEnumerable<MyTfsProjectCollection> ProjectCollections
        {
            get
            {
                try
                {
                    CatalogNode configurationServerNode = _tfsConfigurationServer.CatalogNode;
                    ReadOnlyCollection<CatalogNode> tcpNodes = configurationServerNode.QueryChildren(
                        new[] {CatalogResourceTypes.ProjectCollection},
                        false,
                        CatalogQueryOptions.None
                        );
                    
                    return tcpNodes
                        .Select(tcpNode => new MyTfsProjectCollection(tcpNode, _tfsConfigurationServer, _networkCredential))
                        .Where(i => i.CurrentUserHasAccess);
                } 
                catch (Exception ex)
                {
                    _log.Error("Unable to retrieve project collections", ex);
                    MessageBox.Show("Unable to retrieve project collections. " + ex.Message);
                    return Enumerable.Empty<MyTfsProjectCollection>();
                }
            }
        }

        public void Dispose()
        {
            _tfsConfigurationServer.Dispose();
        }

    }
}
