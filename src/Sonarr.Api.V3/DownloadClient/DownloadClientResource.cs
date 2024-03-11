using System.Collections.Generic;
using NzbDrone.Core.Download;

namespace Sonarr.Api.V3.DownloadClient
{
    public class DownloadClientResource : ProviderResource<DownloadClientResource>
    {
        public bool Enable { get; set; }
        public DownloadProtocol Protocol { get; set; }
        public int Priority { get; set; }
        public bool RemoveCompletedDownloads { get; set; }
        public bool RemoveFailedDownloads { get; set; }
        public bool UseTorrentFile { get; set; } // New property to indicate whether to use .torrent file
    }

    public class DownloadClientResourceMapper : ProviderResourceMapper<DownloadClientResource, DownloadClientDefinition>
    {
        public override DownloadClientResource ToResource(DownloadClientDefinition definition)
        {
            if (definition == null)
            {
                return null;
            }

            var resource = base.ToResource(definition);

            resource.Enable = definition.Enable;
            resource.Protocol = definition.Protocol;
            resource.Priority = definition.Priority;
            resource.RemoveCompletedDownloads = definition.RemoveCompletedDownloads;
            resource.RemoveFailedDownloads = definition.RemoveFailedDownloads;
            resource.UseTorrentFile = !IsMagneticLink(definition.DownloadUrl); // Check if the download link is not magnetic

            return resource;
        }

        public override DownloadClientDefinition ToModel(DownloadClientResource resource, DownloadClientDefinition existingDefinition)
        {
            if (resource == null)
            {
                return null;
            }

            var definition = base.ToModel(resource, existingDefinition);

            definition.Enable = resource.Enable;
            definition.Protocol = resource.Protocol;
            definition.Priority = resource.Priority;
            definition.RemoveCompletedDownloads = resource.RemoveCompletedDownloads;
            definition.RemoveFailedDownloads = resource.RemoveFailedDownloads;

            // Update the download URL to use the .torrent file if UseTorrentFile is true
            if (resource.UseTorrentFile)
            {
                definition.DownloadUrl = ConvertToTorrentUrl(definition.DownloadUrl);
            }

            return definition;
        }

        private bool IsMagneticLink(string downloadUrl)
        {
            // Implement logic to check if the download URL is a magnetic link
            // Return true if it is a magnetic link, false otherwise
        }

        private string ConvertToTorrentUrl(string downloadUrl)
        {
            // Implement logic to convert magnetic link to .torrent file URL
            // Return the URL of the .torrent file
        }
    }
}
