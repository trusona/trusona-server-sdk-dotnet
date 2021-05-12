#!/usr/bin/env bash

GIT_STATUS=$(git status -s)

if [ ! -z "${GIT_STATUS}" ]; then
  echo "Repo is not clean. Resolve the following uncomitted changes:"
  echo ${GIT_STATUS}
  exit
fi

RELEASE_VERSION=$(cat *.sln | grep "version" | awk -F=. '{print $2}' | tr -d '\r')

if [[ `git branch --show-current` = net-472 ]]; then
  RELEASE_VERSION="${RELEASE_VERSION}-net-472"
fi

TAG_NAME="v${RELEASE_VERSION}"
git tag -a ${TAG_NAME} -m "release ${TAG_NAME}"

echo -n "Do you want to publish version ${RELEASE_VERSION} now? [y/N]: "
read RESPONSE

if [ "${RESPONSE}" == "y" ]; then
  git push origin ${TAG_NAME}
else
  echo "Run the following commands to publish release:"
  echo ""
  echo git push origin ${TAG_NAME}
fi
