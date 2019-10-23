# SPOTIFY DEMO

Just playing around with the Spotify API.

A console application that takes in a Spotify username and calculates some metrics (using Spotify's "track features" data) about the tracks in that user's public playlists.

Client ID and Client Secret need to go in `appsettings.json`. Get them by registering an app [here](https://developer.spotify.com/dashboard/applications).

## API REFERENCE

<https://developer.spotify.com/documentation/web-api/reference/>

## Spotify user IDs

Spotify does not allow usernames to be looked up by email address. A user can find their username in the Spotify app by going to Settings --> Account.

![Finding username](FindingUsername.gif)

Spotify does allow public playlists to be queried, and in those public playlists, usernames can be viewed. I found these usernames by querying public playlists. They can be used to test this application.

```
jackshurtleff
kayleehall3221
luw8oybw1obuzkidx1b93k4dg
ldb_1997
1231316454
hyarleque
xgpbjy65q1almq65seqtss5ka
zr037nrv0wedyejme4m394p7u
```
