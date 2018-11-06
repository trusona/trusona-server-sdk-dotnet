# Development

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
