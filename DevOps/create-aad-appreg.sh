#!/bin/bash

# Get app display name from command-line args
APP=$1

# Check if app registration exists
AADAPPREG=$(az ad app list --display-name $APP)
if [ $(echo $AADAPPREG | jq length) == '0' ]
then
    # App registration does not exist -> create it
    AADAPPREG=$(az ad app create --display-name AzureWebSampleUI --available-to-other-tenants false \
        --identifier-uris https://azure-web-sample.net)
else
    # App registration exists -> we do not need to create a new one
    AADAPPREG=$(echo $AADAPPREG | jq '.[0]')
fi
# Get app id and object id from app registration JSON
APPID=$(jq '.appId' -r <<< $AADAPPREG)
OBJID=$(jq '.objectId' -r <<< $AADAPPREG)
# Set redirect URI for SPA app (with retry logic because AAD does not accept
# this update immediately after AAD app has been registered; takes a few secs)
until az rest --method PATCH --uri https://graph.microsoft.com/v1.0/applications/$OBJID \
    --headers 'Content-Type=application/json' --body "{spa:{redirectUris:['http://localhost:4200']}}" \
    > /dev/null 2>&1; do
    sleep 3s
done

# Check if service principal exists
AADSP=$(az ad sp list --display-name $APP)
if [ $(echo $AADSP | jq length) == '0' ]
then
    # Service principal does not exist -> create it
    AADSP=$(az ad sp create --id $APPID)
else
    # Service principal exists -> we do not need to create a new one
    AADSP=$(echo $AADSP | jq '.[0]')
fi
# Reset app secret to get a new one
APPCRED=$(az ad app credential reset --id $APPID)
APPSECRET=$(jq '.password' -r <<< $APPCRED)
TENANT=$(jq '.tenant' -r <<< $APPCRED)

# Return results as JSON
jq --arg key0 'appId' --arg value0 $APPID \
   --arg key1 'appSecret' --arg value1 $APPSECRET \
   --arg key2 'tenant' --arg value2 $TENANT \
   --arg key3 'objectId' --arg value3 $OBJID \
    '. | .[$key0]=$value0 | .[$key1]=$value1 | .[$key2]=$value2 | .[$key3]=$value3' \
   <<<'{}'

# Cleanup command; just in case you need it for debugging purposes
# az ad app delete --id $(az ad app list --display-name $APP | jq .[0].appId -r)
