﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using ToastifyAPI.GitHub.Model;

namespace ToastifyAPI.GitHub
{
    public static class Releases
    {
        /// <summary>
        /// Get a published release with the specified tag.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="repo"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Release GetReleaseByTagName(this GitHubAPI api, RepoInfo repo, string tag)
        {
            string url = api.GetFullEndpointUrl($"/repos/:owner/:repo/releases/tags/{tag}", repo);
            return api.DownloadJson<Release>(url);
        }

        /// <summary>
        /// View the latest published full release for the repository. Draft releases and prereleases are not returned by this endpoint.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static Release GetLatestRelease(this GitHubAPI api, RepoInfo repo)
        {
            string url = api.GetFullEndpointUrl("/repos/:owner/:repo/releases/latest", repo);
            return api.DownloadJson<Release>(url);
        }

        /// <summary>
        /// This returns a list of releases, which does not include regular Git tags that have not been associated with a release.
        /// <para />
        /// Information about published releases are available to everyone. Only users with push access will receive listings for draft releases.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static List<Release> GetReleases(this GitHubAPI api, RepoInfo repo)
        {
            string url = api.GetFullEndpointUrl("/repos/:owner/:repo/releases", repo);
            CollectionData<Release> releases = api.DownloadCollectionJson<Release>(url);
            return releases.HttpStatusCode == HttpStatusCode.OK ? releases.Collection.ToList() : new List<Release>();
        }

        public static string GetUrlOfLatestRelease(RepoInfo repo)
        {
            return repo.Format("https://github.com/:owner/:repo/releases/latest");
        }
    }
}