# Development

## Running the integration tests

Grab the TruBank Server token and secret for UAT from 1password.

```bash
export TRUSONA_TOKEN=<token>
export TRUSONA_SECRET=<secret>
```

In your IDE/editor, find all places where we've commented out the `[Fact]` annotation and uncomment them

From the root of the project, run:

```bash
dotnet test TrusonaSDK.Test
```

Don't forget to re-comment out the tests:

```bash
git checkout -- .
```

## Releasing

### Bumping the version

In Visual Studio, right click on the `trusona-server-sdk-dotnet` top level solution and click `Options`

Under `Main Settings`, set the `Version` and click `OK`

Verify there are two changed files by running `git status`

```bash
	modified:   TrusonaSDK.API/TrusonaSDK.API.csproj
	modified:   trusona-server-sdk-dotnet.sln
```

Commit and push your changes

### Deploying

1. Ensure a clean working directory
1. `./release.sh`