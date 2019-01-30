using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.RavenDb
{
    public interface IOnBeforeStore
    {
        void Handle(object sender, BeforeStoreEventArgs e);
    }
}
