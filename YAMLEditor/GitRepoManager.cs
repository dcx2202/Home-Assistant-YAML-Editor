﻿using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using System;
using System.Collections.Generic;

namespace YAMLEditor
{
    static class GitRepoManager
    {
        /// <summary>
        /// Clones a repository
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="remoterepo_path"></param>
        /// <param name="localrepo_path"></param>
        public static void Clone(string email, string password, string remoterepo_path, string localrepo_path)
        {
            var co = new CloneOptions();
            co.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = email, Password = password };

            if (!remoterepo_path.EndsWith(".git"))
                remoterepo_path += ".git";

            Repository.Clone(remoterepo_path, localrepo_path, co);
        }

        /// <summary>
        /// Pulls changes
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="localrepo"></param>
        public static void Pull(string email, string password, string localrepo_path)
        {
            using (var repo = new Repository(localrepo_path))
            {
                // Credential information to fetch
                LibGit2Sharp.PullOptions options = new LibGit2Sharp.PullOptions();
                options.FetchOptions = new FetchOptions();
                options.FetchOptions.CredentialsProvider = new CredentialsHandler(
                    (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = email,
                            Password = password
                        });

                // User information to create a merge commit
                var signature = new LibGit2Sharp.Signature(
                    new Identity(email, email), DateTimeOffset.Now);

                // Pull
                LibGit2Sharp.Commands.Pull(repo, signature, options);
            }
        }

        /// <summary>
        /// Pushes changes
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="localrepo_path"></param>
        public static void Push(string email, string password, string localrepo_path)
        {
            using (var repo = new Repository(localrepo_path))
            {
                Remote remote = repo.Network.Remotes["origin"];
                var options = new PushOptions();
                options.CredentialsProvider = (_url, _user, _cred) =>
                    new UsernamePasswordCredentials { Username = email, Password = password };
                repo.Network.Push(remote, @"refs/heads/master", options);
            }
        }

        /// <summary>
        /// Stages files and commits
        /// </summary>
        /// <param name="email"></param>
        /// <param name="localrepo_path"></param>
        /// <param name="files"></param>
        public static void Commit(string email, string localrepo_path, List<string> files)
        {
            using (var repo = new Repository(localrepo_path))
            {
                foreach (string file in files)
                {
                    // Stage the file
                    LibGit2Sharp.Commands.Stage(repo, file);
                }

                // Create the committer's signature and commit
                Signature author = new Signature(email, email, DateTime.Now);
                Signature committer = author;

                // Commit to the repository
                Commit commit = repo.Commit("Commit from YAMLEditor", author, committer);
            }
        }
    }
}

