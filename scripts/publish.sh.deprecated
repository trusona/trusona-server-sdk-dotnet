#!/usr/bin/env bash

git_revision=$(git rev-parse --short HEAD)
git_branch=$(git rev-parse --abbrev-ref HEAD)
package_version=$(cat *.sln | grep "version" | awk -F=. '{print $2}' | tr -d '\r')

if [ "${1}" != "release" ]; then
  package_version="${package_version}-${git_revision}"
fi

dotnet pack /p:PackageVersion=${package_version} \
            --configuration ${1} \
            --output ../nupkgs \
            --no-build \
            --no-restore

#dotnet nuget sign nupkgs/*.nupkg \
#            --CertificatePath trusona.p12 \
#            --CertificatePassword B242E50F-0194-43BD-AF83-265B004AEB83 \
#            --Timestamper http://sha256timestamp.ws.symantec.com/sha256/timestamp \

dotnet nuget push nupkgs/*.nupkg \
            --source https://trusona.jfrog.io/trusona/api/nuget/nuget-local \
            --api-key "${NUGET_JFROG_USER}:${NUGET_JFROG_API_KEY}"

#dotnet nuget push nupkgs/*.nupkg \
#            --source https://api.nuget.org/v3/index.json \
#            --api-key ${NUGET_ORG_API_KEY}
