using System;
using System.Runtime.InteropServices;
using Framework.Extensions;
using Microsoft.SharePoint;

namespace Framework.SharePoint.Extensions
{
    public static class SPFeatureReceiverExtension
    {
        public static Guid GetReceiverId(this SPFeatureReceiver featureReceiver)
        {
            var guidAttribute = featureReceiver.GetAttribute<GuidAttribute>();
            return guidAttribute != null ? new Guid(guidAttribute.Value) : Guid.Empty;
        }
    }
}